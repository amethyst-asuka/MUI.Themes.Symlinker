Imports FirstFloor.ModernUI.Windows.Controls
Imports MUI.Themes.Symlinker.hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Public Partial Class MainWindow
	Inherits ModernWindow
	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub ModernWindow_Initialized(sender As Object, e As EventArgs)
		If VistaSecurity.IsAdmin() Then
			Me.Title += " (Elevated)"
		End If
	End Sub
End Class
