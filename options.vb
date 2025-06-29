Imports System.IO

Public Class options
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim writer As TextWriter = New StreamWriter("popup.config")

            writer.Write("True")

            writer.Close()

        Finally

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim writer As TextWriter = New StreamWriter("popup.config")

            writer.Write("false")

            writer.Close()

        Finally

        End Try
    End Sub
End Class