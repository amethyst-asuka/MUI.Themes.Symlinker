Imports Microsoft.Win32.SafeHandles
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Something.FileSystem
	Public NotInheritable Class SymbolicLink
		Private Sub New()
		End Sub
		Private Const genericReadAccess As UInteger = &H80000000UI

		Private Const fileFlagsForOpenReparsePointAndBackupSemantics As UInteger = &H2200000

		Private Const ioctlCommandGetReparsePoint As Integer = &H900a8

		Private Const openExisting As UInteger = &H3

		Private Const pathNotAReparsePointError As UInteger = &H80071126UI

		Private Const shareModeAll As UInteger = &H7
		' Read, Write, Delete
		Private Const symLinkTag As UInteger = &Ha000000cUI

		Private Const targetIsAFile As Integer = 0

		Private Const targetIsADirectory As Integer = 1

		<DllImport("kernel32.dll", SetLastError := True)> _
		Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As UInteger, dwShareMode As UInteger, lpSecurityAttributes As IntPtr, dwCreationDisposition As UInteger, dwFlagsAndAttributes As UInteger, _
			hTemplateFile As IntPtr) As SafeFileHandle
		End Function

		<DllImport("kernel32.dll", SetLastError := True)> _
		Private Shared Function CreateSymbolicLink(lpSymlinkFileName As String, lpTargetFileName As String, dwFlags As Integer) As Boolean
		End Function

		<DllImport("kernel32.dll", CharSet := CharSet.Auto, SetLastError := True)> _
		Private Shared Function DeviceIoControl(hDevice As IntPtr, dwIoControlCode As UInteger, lpInBuffer As IntPtr, nInBufferSize As Integer, lpOutBuffer As IntPtr, nOutBufferSize As Integer, _
			ByRef lpBytesReturned As Integer, lpOverlapped As IntPtr) As Boolean
		End Function

		<StructLayout(LayoutKind.Sequential)> _
		Public Structure SymbolicLinkReparseData
			' Not certain about this!
			Private Const maxUnicodePathLength As Integer = 260 * 2

			Public ReparseTag As UInteger
			Public ReparseDataLength As UShort
			Public Reserved As UShort
			Public SubstituteNameOffset As UShort
			Public SubstituteNameLength As UShort
			Public PrintNameOffset As UShort
			Public PrintNameLength As UShort
			Public Flags As UInteger

			<MarshalAs(UnmanagedType.ByValArray, SizeConst := maxUnicodePathLength)> _
			Public PathBuffer As Byte()
		End Structure

		Public Shared Sub CreateDirectoryLink(linkPath As String, targetPath As String)
			If Not CreateSymbolicLink(linkPath, targetPath, targetIsADirectory) OrElse Marshal.GetLastWin32Error() <> 0 Then
				Try
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error())
				Catch exception As COMException
					Throw New IOException(exception.Message, exception)
				End Try
			End If
		End Sub

		Public Shared Sub CreateFileLink(linkPath As String, targetPath As String)
			If Not CreateSymbolicLink(linkPath, targetPath, targetIsAFile) Then
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error())
			End If
		End Sub

		Public Shared Function Exists(path As String) As Boolean
			If Not Directory.Exists(path) AndAlso Not File.Exists(path) Then
				Return False
			End If
			Dim target As String = GetTarget(path)
			Return target IsNot Nothing
		End Function

		Private Shared Function getFileHandle(path As String) As SafeFileHandle
			Return CreateFile(path, genericReadAccess, shareModeAll, IntPtr.Zero, openExisting, fileFlagsForOpenReparsePointAndBackupSemantics, _
				IntPtr.Zero)
		End Function

		Public Shared Function GetTarget(path As String) As String
			Dim reparseDataBuffer As SymbolicLinkReparseData

			Using fileHandle As SafeFileHandle = getFileHandle(path)
				If fileHandle.IsInvalid Then
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error())
				End If

				Dim outBufferSize As Integer = Marshal.SizeOf(GetType(SymbolicLinkReparseData))
				Dim outBuffer As IntPtr = IntPtr.Zero
				Try
					outBuffer = Marshal.AllocHGlobal(outBufferSize)
					Dim bytesReturned As Integer
					Dim success As Boolean = DeviceIoControl(fileHandle.DangerousGetHandle(), ioctlCommandGetReparsePoint, IntPtr.Zero, 0, outBuffer, outBufferSize, _
						bytesReturned, IntPtr.Zero)

					fileHandle.Close()

					If Not success Then
						If CUInt(Marshal.GetHRForLastWin32Error()) = pathNotAReparsePointError Then
							Return Nothing
						End If
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error())
					End If

					reparseDataBuffer = CType(Marshal.PtrToStructure(outBuffer, GetType(SymbolicLinkReparseData)), SymbolicLinkReparseData)
				Finally
					Marshal.FreeHGlobal(outBuffer)
				End Try
			End Using
			If reparseDataBuffer.ReparseTag <> symLinkTag Then
				Return Nothing
			End If

			Dim target As String = Encoding.Unicode.GetString(reparseDataBuffer.PathBuffer, reparseDataBuffer.PrintNameOffset, reparseDataBuffer.PrintNameLength)

			Return target
		End Function
	End Class
End Namespace
