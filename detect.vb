Imports System.IO

Public Class detect
    Private Sub detect_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try
            RichTextBox1.LoadFile("mlwnotif.log", RichTextBoxStreamType.PlainText)

        Catch

        End Try


        labelheure.Text = DateTime.Now
        Timer1.Start()
        Timer2.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Close()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label4.Text = RichTextBox1.Text
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class