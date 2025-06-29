Public Class Form2
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim mBatteryLifePct As Integer                                                          'contient la durée de vie restante de la batterie en %
        Dim mBatteryLifeRemaining As Integer                                                    'contient la durée de vie restante de la batterie en sec
        Dim powerlifestatut As String
        Dim pwstat As String
        Dim pwrestant As Integer

        mBatteryLifePct = SystemInformation.PowerStatus.BatteryLifePercent * 100                'recupération du pourcentage
        mBatteryLifeRemaining = SystemInformation.PowerStatus.BatteryLifeRemaining            'recupération de la durée en sec
        powerlifestatut = SystemInformation.PowerStatus.BatteryFullLifetime
        pwstat = SystemInformation.PowerStatus.BatteryChargeStatus.ToString()
        pwrestant = SystemInformation.PowerStatus.BatteryLifeRemaining




        Label1.Text = mBatteryLifePct.ToString() & "% de batterie ( " & mBatteryLifeRemaining.ToString() & " s restant )"
        Label3.Text = powerlifestatut.ToString()
        Label4.Text = pwstat.ToString()
        Label6.Text = pwrestant.ToString()
        ProgressBar1.Value = mBatteryLifePct

        Label8.ImageIndex = 0



        If ProgressBar1.Value < 100 Then

            Label8.ImageIndex = 1

        End If

        If ProgressBar1.Value < 80 Then

            Label8.ImageIndex = 2

        End If

        If ProgressBar1.Value < 50 Then

            Label8.ImageIndex = 3

        End If

        If ProgressBar1.Value < 20 Then

            Label8.ImageIndex = 4

        End If
        Dim charge As Integer

        charge = 0

        charge = SystemInformation.PowerStatus.BatteryChargeStatus
        If charge = 8 Then
            Label8.ImageIndex = 5
        End If

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class