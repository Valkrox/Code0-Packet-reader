Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports System.Management
Imports System.Net.NetworkInformation


Public Class Form1
    Dim clientVersion As Double = 1.0
    Dim updadePath As String
    Dim packageFile As String
    Dim webclient As WebClient


    'end


    Dim firewallpkreader As Boolean = True
    Dim Vle
    Dim socketz As New Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP)
    Dim bytedata(4096) As Byte
    Dim myip As IPAddress
    Dim started As Boolean = True
    Dim sizediff As Size
    Dim formloaded As Boolean = False
    Dim FilterIPAddress As New IPAddress(0)
    Dim FilterIP As Boolean
    Dim mycomputerconnections() As Net.NetworkInformation.NetworkInterface




    'DGV Update stuff
    Dim stringz As String = ""
    Dim unstringz As String = ""
    Dim cryptstringz As String = ""
    Dim Typez As String = ""
    Dim port As String = ""
    Dim destport As String = ""
    Dim ipfrom As IPAddress
    Dim ipto As IPAddress
    Dim destinationport As UInteger
    Dim sourceport As UInteger
    Dim total As Integer
    Dim suspectpacket As Integer
    Dim suspectip As Integer
    Dim vuln As Integer
    Dim IPverif As IPAddress
    Dim nbrpacketnull As Integer
    Dim safe As Integer
    Dim ImgList As New ImageList
    Dim yourip As New IPAddress(0)
    Dim nbrpacket As Integer
    Dim instance As Boolean = False
    Dim prot As String
    Dim protsrs As String
    Dim pckt As Integer = 0
    Dim pckttemp As Integer = 0
    Dim pckrecord As Integer = 0
    Dim lignetotal As Integer = 0
    Dim ipsecure As String
    Dim ipsecure2 As String
    Dim IPV4verif As String
    Dim socketexist As Boolean = False





#Disable Warning









#Enable Warning



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        creator.Show()
        Timer11.Start()
        Timer12.Start()
        Timer13.Start()
        Timer14.Start()
        Timer15.Start()
        Timer16.Start()
        Timer10.Start()
        Timer17.Start()
        Timer18.Start()
        Timer19.Start()
        Timer20.Start()
        Timer21.Start()

        Me.Text = "Code0 by Storm | Diagnostique et vérification du système | " & My.Computer.Info.OSFullName.ToString() & " " & My.Computer.Info.OSVersion & " " & My.Computer.Info.InstalledUICulture.ToString()

        Timer9.Start()
        Timer8.Start()

        Try
            If (File.Exists("autoclear.conf")) Then
                Label28.Text = "Oui"

                autoclear.Start()
            End If


        Catch ex As Exception

        End Try




        ListView1.Columns.Add("Programs", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Full Path", 320, HorizontalAlignment.Left)
        ListView1.SmallImageList = ImgList 'Uses the ImgList for the icons. 
        ListView1.FullRowSelect = True
        ListView1.View = View.Details
        ListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable

        sizediff.Height = Me.Height - DGV.Height
        sizediff.Width = Me.Width - DGV.Width
        formloaded = True

        mycomputerconnections = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces

        For i = 0 To mycomputerconnections.Length - 1
            ComboBox1.Items.Add(mycomputerconnections(i).Name + " " + mycomputerconnections(i).Description)
        Next

        Const NUMBER_OF_ROWS_TO_ALLOW As Integer = 80

        DGV.AllowUserToAddRows = DGV.RowCount <= NUMBER_OF_ROWS_TO_ALLOW

        DGV.AllowUserToAddRows = False

        Timer1.Start()

        Try
            RichTextBox1.LoadFile("résultscan.log", RichTextBoxStreamType.PlainText)

        Catch
            Dim writer As TextWriter = New StreamWriter("résultscan.log")

            writer.Write(RichTextBox1.Text)

            writer.Close()
        End Try

        Timer3.Start()
        Timer5.Start()

        TCP_UDPstat.Visible = True


        ' Set the selection background color for all the cells.
        DGV.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DGV.DefaultCellStyle.SelectionForeColor = Color.Lime


        ' Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
        ' value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
        DGV.RowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))

        ' Set the background color for all rows and for alternating rows. 
        ' The value for alternating rows overrides the value for all rows. 
        DGV.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DGV.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))

        ' Set the row and column header styles.
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Lime
        DGV.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DGV.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))





    End Sub






    Private Sub OnReceive(ByVal asyncresult As IAsyncResult)

        If started = True Then
            'Get Length of packet (including header)
            Dim readlength As UInteger = BitConverter.ToUInt16(Byteswap(bytedata, 2), 0)
            sourceport = BitConverter.ToUInt16(Byteswap(bytedata, 22), 0)
            destinationport = BitConverter.ToUInt16(Byteswap(bytedata, 24), 0)


            'Get Protocol Type
            If bytedata(9) = 6 Then
                Typez = "TCP"
            ElseIf bytedata(9) = 17 Then
                Typez = "UDP"
            Else
                Typez = "???"
            End If

            'Get IP from and to
            ipfrom = New IPAddress(BitConverter.ToUInt32(bytedata, 12))
            ipto = New IPAddress(BitConverter.ToUInt32(bytedata, 16))
            port = sourceport
            destport = destinationport


            'If this is a packet to/from me and not from myself then...
            If (ipfrom.Equals(myip) = True Or ipto.Equals(myip) = True) And ipto.Equals(ipfrom) = False Then
                If FilterIP = False Or (FilterIP = True And (FilterIPAddress.Equals(ipfrom) Or FilterIPAddress.Equals(ipto))) Then

                    'Fix data
                    stringz = ""
                    For i = 26 To readlength - 1
                        If Char.IsLetterOrDigit(Chr(bytedata(i))) + Char.IsSymbol(Chr(bytedata(i))) + Char.IsUpper(Chr(bytedata(i))) + Char.IsHighSurrogate(Chr(bytedata(i))) + Char.IsLowSurrogate(Chr(bytedata(i))) +
                            Char.IsSeparator(Chr(bytedata(i))) + Char.IsSurrogate(Chr(bytedata(i))) + Char.IsControl(Chr(bytedata(i))) + Char.IsWhiteSpace(Chr(bytedata(i))) + Char.IsHighSurrogate(Chr(bytedata(i))) = True Then
                            stringz = stringz & Chr(bytedata(i))
                        Else
                            stringz = stringz & "."
                        End If
                    Next

                    cryptstringz = ""
                    For i = 26 To readlength - 1
                        If Char.IsDigit(Chr(bytedata(i))) = True Then
                            cryptstringz = cryptstringz & Chr(bytedata(i))
                        Else
                            cryptstringz = cryptstringz & "."
                        End If
                    Next

                    unstringz = ""
                    For i = 26 To readlength - 1
                        If Char.IsDigit(Chr(bytedata(i))) = True Then
                            unstringz = unstringz & Chr(bytedata(i))
                        Else
                            unstringz = unstringz & "."
                        End If
                    Next

                    'Put data to DataGridView, since it's on a different thread now, invoke it
                    Try
                        DGV.Invoke(New MethodInvoker(AddressOf DGVUpdate))

                    Catch

                    End Try

                End If
            End If

        End If

        'Restart the Receiving
        Try

            socketz.BeginReceive(bytedata, 0, bytedata.Length, SocketFlags.None, New AsyncCallback(AddressOf OnReceive), Nothing)

        Catch

        End Try
    End Sub

    Private Sub DGVUpdate()
        pckttemp = pckttemp + 1
        pckt = pckt + 1
        'Remove rows if there are too many
        If DGV.Rows.Count > ProgressBar1.Value Then
            DGV.Rows.RemoveAt(0)
        End If

        If DGV2.Rows.Count > ProgressBar1.Value Then
            DGV2.Rows.RemoveAt(0)
        End If

        prot = ""
        protsrs = ""

        ''Get protocole

        If (port = 80) Then
            prot = "HTTP"
        End If
        If (destinationport = 80) Then
            protsrs = "HTTP"
        End If
        If (port = 20) Then
            prot = "ftp data"
        End If
        If (destinationport = 20) Then
            protsrs = "ftp data"
        End If
        If (port = 21) Then
            prot = "ftp"
        End If
        If (destinationport = 21) Then
            protsrs = "ftp"
        End If
        If (port = 23) Then
            prot = "telnet"
        End If
        If (destinationport = 23) Then
            protsrs = "telnet"
        End If
        If (port = 25) Then
            prot = "smtp"
        End If
        If (destinationport = 25) Then
            protsrs = "smtp"
        End If
        If (port = 22) Then
            prot = "ssh"
        End If
        If (destinationport = 22) Then
            protsrs = "ssh"
        End If
        If (port = 53) Then
            prot = "domain"
        End If
        If (destinationport = 53) Then
            protsrs = "domain"
        End If
        If (port = 1900) Then
            prot = "SSDP"
        End If
        If (destinationport = 1900) Then
            protsrs = "SSDP"
        End If
        If (port = 109) Then
            prot = "pop2"
        End If
        If (destinationport = 109) Then
            protsrs = "pop2"
        End If
        If (port = 110) Then
            prot = "pop2"
        End If
        If (destinationport = 110) Then
            protsrs = "pop3"
        End If
        If (port = 443) Then
            prot = "HTTPS"
        End If
        If (destinationport = 443) Then
            protsrs = "HTTPS"
        End If
        If (port = 7) Then
            prot = "echo"
        End If
        If (destinationport = 7) Then
            protsrs = "echo"
        End If
        If (port = 70) Then
            prot = "gopher"
        End If
        If (destinationport = 70) Then
            protsrs = "gopher"
        End If
        If (port = 993) Then
            prot = "SSL-IMAP"
        End If
        If (destinationport = 993) Then
            protsrs = "SSL-IMAP"
        End If
        If (port = 70) Then
            prot = "Gopher"
        End If
        If (destinationport = 70) Then
            protsrs = "Gopher"
        End If


        '' End get protocole
        Try

            Dim ips() As IPAddress = Dns.GetHostAddresses(Dns.GetHostName)
            For Each ipa As IPAddress In ips
                If ipa.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    IPV4verif = ipa.ToString()
                End If
            Next
        Catch

        End Try

        Try

            Label113.Text = IPV4verif
        Catch

        End Try

        Try
            ipsecure = ipfrom.ToString()
        Catch

        End Try

        Try

            ipsecure2 = ipto.ToString()
        Catch

        End Try


        Try


            If CheckBox1.Checked = True Then
                If ipsecure = IPV4verif Then
                    ipsecure = "Mon IP"
                End If
            End If

        Catch

        End Try

        Try

            If CheckBox1.Checked = True Then
                If ipsecure2 = IPV4verif Then
                    ipsecure2 = "Mon IP"
                End If
            End If
        Catch

        End Try


        DGV.Rows.Add()
        DGV.Rows(DGV.Rows.Count - 1).Cells(0).Value = port
        DGV.Rows(DGV.Rows.Count - 1).Cells(1).Value = destinationport






        DGV.Rows(DGV.Rows.Count - 1).Cells(2).Value = ipsecure 'From Column, size at 125
        DGV.Rows(DGV.Rows.Count - 1).Cells(3).Value = ipsecure2 'To Column, size at 125
        DGV.Rows(DGV.Rows.Count - 1).Cells(4).Value = prot
        DGV.Rows(DGV.Rows.Count - 1).Cells(5).Value = protsrs
        DGV.Rows(DGV.Rows.Count - 1).Cells(6).Value = Typez 'Type Column, size at 50
        DGV.Rows(DGV.Rows.Count - 1).Cells(7).Value = stringz 'Data column, size mode set to fill
        DGV.Rows(DGV.Rows.Count - 1).Cells(8).Value = cryptstringz 'Data column, size mode set to fill

        DGV2.Rows.Add()
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(0).Value = port
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(1).Value = destinationport
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(2).Value = ipsecure & ":" & sourceport 'From Column, size at 125
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(3).Value = ipsecure2 & ":" & destinationport 'To Column, size at 125
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(4).Value = Typez 'Type Column, size at 50
        DGV2.Rows(DGV2.Rows.Count - 1).Cells(5).Value = unstringz 'Data column, size mode set to fill



        If (port = "") Then
            nbrpacketnull = nbrpacketnull + 1
            Label65.Text = "nombre de packet invalide : " & nbrpacketnull.ToString()
            Return
        End If

        nbrpacket = nbrpacket + 1

        total = total + 1
        Label61.Text = "nombre de packet vérifier : " & total.ToString()

        If port = "42" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 42 Détécté : Type : Trojan Wins, Niveau de menace 5/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 42")

            writer.Close()



        End If



        If destinationport = "42" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 42 Détécté : Type : Trojan Wins, Niveau de menace 5/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 42")

            writer.Close()
        End If


        If port = "41" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 41 Détécté : Type : Trojan (DeepThroat, Backdoor), Niveau de menace 7/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 41")

            writer.Close()
        End If

        If destinationport = "41" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 41 Détécté : Type : Trojan (DeepThroat, Backdoor), Niveau de menace 7/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 41")

            writer.Close()
        End If

        If port = "81" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 81 Détécté : Type : Trojan (W32.beagle, Xeory, Asylum), Niveau de menace 7/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 81")

            writer.Close()
        End If

        If destinationport = "81" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 81 Détécté : Type : Trojan (W32.beagle, Xeory, Asylum), Niveau de menace 7/10 Solution : Bloquez le port via le parfeu  + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 81")

            writer.Close()
        End If

        If port = "999" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 999 Détécté : Type : Trojan (DeepThroat (version keyloger), Garcon, chat power, Winsatan), Niveau de menace 9/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 999")

            writer.Close()
        End If

        If destinationport = "999" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 999 Détécté : Type : Trojan (DeepThroat (version keyloger), Garcon, chat power, Winsatan), Niveau de menace 9/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 999")

            writer.Close()
        End If

        If port = "0" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 0 Détécté : Type : Trojan (ReX), Niveau de menace 6.5/10 Solution : Vérifier les tôt d'aparaision (si il revient beaucoup de fois bloquez le port via le parfeu)"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 0")

            writer.Close()
        End If

        If destinationport = "0" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 0 Détécté : Type : Trojan (ReX), Niveau de menace 6.5/10 Solution : Vérifier les tôt d'aparaision (si il revient beaucoup de fois bloquez le port via le parfeu)"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 0")

            writer.Close()
        End If

        If port = "1044" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1044 Détécté : Type : Trojan (Ptakks (backdoor)), Niveau de menace 7.5/10 Solution : Vérifier les tôt d'aparaision (si il revient beaucoup de fois bloquez le port via le parfeu + analise antivirus)"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1044")

            writer.Close()
        End If

        If destinationport = "1044" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1044 Détécté : Type : Trojan (Ptakks (backdoor)), Niveau de menace 7.5/10 Solution : Vérifier les tôt d'aparaision (si il revient beaucoup de fois bloquez le port via le parfeu + analise antivirus)"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1044")

            writer.Close()
        End If

        If port = "5353" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5353 Détécté : Type : Trojan (Webramp controle), Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5353")

            writer.Close()
        End If

        If destinationport = "5353" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5353 Détécté : Type : Trojan (Webramp controle), Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5353")

            writer.Close()
        End If

        If port = "3150" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3150 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3150")

            writer.Close()
        End If

        If destinationport = "3150" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3150 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3150")

            writer.Close()
        End If

        If port = "2140" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 2140 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 2140")

            writer.Close()
        End If

        If destinationport = "2140" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 2140 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 2140")

            writer.Close()
        End If

        If port = "6671" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 6671 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 6671")

            writer.Close()
        End If

        If destinationport = "6671" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 6671 Détécté : Type : Trojan Deepthroat, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu + analise antivirus."
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 6671")

            writer.Close()
        End If

        If port = "1900" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1900 Détécté : Type : Trojan Xeros Phaser, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1900")

            writer.Close()
        End If

        If destinationport = "1900" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1900 Détécté : Type : Trojan Xeros Phaser, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1900")

            writer.Close()
        End If

        If port = "53" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 53 Détécté : Type : Trojan W32.spybot, ADMworm, Lion, W32.Dasher, Civicat, Esteems, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 53")

            writer.Close()
        End If

        If destinationport = "53" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 53 Détécté : Type : Trojan W32.spybot, ADMworm, Lion, W32.Dasher, Civicat, Esteems, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 53")

            writer.Close()
        End If

        If port = "60000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 60000 Détécté : Type : Trojan RAT Deepthroat, Foreplay, Socket de troies, WiNNUke eXtreame, MiniBacklash, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 60000")

            writer.Close()
        End If

        If destinationport = "60000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 60000 Détécté : Type : Trojan RAT Deepthroat, Foreplay, Socket de troies, WiNNUke eXtreame, MiniBacklash, Niveau de menace 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 60000")

            writer.Close()
        End If

        If port = "901" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 901 Détécté : Type : Trojan NetDevil, Net-Devil Pest, SWAT, ISS,  Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 901")

            writer.Close()
        End If

        If destinationport = "901" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 901 Détécté : Type : Trojan NetDevil, Net-Devil Pest, SWAT, ISS, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 901")

            writer.Close()
        End If

        If port = "2040" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 2040 Détécté : Type : Trojan Inferno uploader,  Niveau de menace 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 2040")

            writer.Close()
        End If

        If destinationport = "2040" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 2040 Détécté : Type : Trojan Inferno uploader, Niveau de menace 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 2040")

            writer.Close()
        End If

        If port = "1045" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1045 Détécté : Type : Trojan Rasmin trojan, Rasmin,  Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1045")

            writer.Close()
        End If

        If destinationport = "1045" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1045 Détécté : Type : Trojan Rasmin trojan, Rasmin, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1045")

            writer.Close()
        End If

        If port = "65535" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65535 Détécté : Type : Trojan RC1, Sins, Adore worm, ShitHeep, RC, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 65535")

            writer.Close()
        End If

        If destinationport = "65535" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65535 Détécté : Type :  Trojan RC1, Sins, Adore worm, ShitHeep, RC, Niveau de menace 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("rmlwnotif.log")

            writer.Write("port 65535")

            writer.Close()
        End If

        If destinationport = "4961" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4961 Détécté : Type :  Non-autorisé IANA "
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("rmlwnotif.log")

            writer.Write("port 4961")

            writer.Close()
        End If

        If port = "4961" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4961 Détécté : Type :  Non-autorisé IANA "
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("rmlwnotif.log")

            writer.Write("port 4961")

            writer.Close()
        End If

        If port = "7676" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 7676 Détécté : Type : Trojan RAT Aqumin AlphaVision, Malware Neoturk, Attaque de routeur ! Niveau de menace 10/10 Solution : Bloquez imperativement le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 7676")

            writer.Close()
        End If

        If destinationport = "7676" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 7676 Détécté : Type :  Trojan RAT Aqumin AlphaVision, Malware Neoturk, Attaque de routeur ! Niveau de menace 10/10 Solution : Bloquez imperativement le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 7676")

            writer.Close()
        End If

        If port = "171" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 171 Détécté : Type : Trojan A trojan, Niveau de menace 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 171")

            writer.Close()
        End If

        If destinationport = "171" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 171 Détécté : Type : Trojan A trojan, Niveau de menace 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 171")

            writer.Close()


        End If

        If port = "464" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 464 Détécté : Type : DDOS : Ce port est assigné a Kerberos mais est utilisé pour les DDOS 9.5/10 Solution : Si beaucoup de requete sont envoyer Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 464")

            writer.Close()
        End If

        If destinationport = "464" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 464 Détécté : Type : DDOS : Ce port est assigné a Kerberos mais est utilisé pour les DDOS 9.5/10 Solution : Si beaucoup de requete sont envoyer Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 464")

            writer.Close()
        End If

        If port = "5000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5000 Détécté : Type : Trojan : Back Door Setup, Bubbel, Ra1d, Sockets des Troie, BioNet Lite, Blazer5, ICKiller, Webus, W32.Bobax, W32.Mytob, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5000")

            writer.Close()
        End If

        If destinationport = "5000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5000 Détécté : Type : Trojan : Back Door Setup, Bubbel, Ra1d, Sockets des Troie, BioNet Lite, Blazer5, ICKiller, Webus, W32.Bobax, W32.Mytob, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5000")

            writer.Close()

        End If

        If port = "5000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5000 Détécté : Type : Trojan : Back Door Setup, Bubbel, Ra1d, Sockets des Troie, BioNet Lite, Blazer5, ICKiller, Webus, W32.Bobax, W32.Mytob, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5000")

            writer.Close()
        End If

        If destinationport = "5000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5000 Détécté : Type : Trojan : Back Door Setup, Bubbel, Ra1d, Sockets des Troie, BioNet Lite, Blazer5, ICKiller, Webus, W32.Bobax, W32.Mytob, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5000")

            writer.Close()

        End If

        If port = "5358" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5358 Détécté : Type : Trojan : Trojan.win32.monder.gen(a.k.a Trojan.Vundo), 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5358")

            writer.Close()
        End If

        If destinationport = "5358" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5358 Détécté : Type : Trojan : Trojan.win32.monder.gen(a.k.a Trojan.Vundo), 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5358")

            writer.Close()

        End If

        If port = "138" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 138 Détécté : Type : Trojan : Chode, Nimda, W32.Spybot 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 138")

            writer.Close()
        End If

        If destinationport = "138" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 138 Détécté : Type : Trojan : Chode, Nimda, W32.Spybot, 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 138")

            writer.Close()

        End If

        If port = "3587" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3587 Détécté : Type : Trojan : ShitHead trojan, 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3587")

            writer.Close()
        End If

        If destinationport = "3587" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3587 Détécté : Type : Trojan : ShitHead trojan, 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3587")

            writer.Close()

        End If

        If port = "3333" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3333 Détécté : Type : Trojan RAT : Daodan, W32.Bratle, W32.Zotob, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3333")

            writer.Close()
        End If

        If destinationport = "3333" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3333 Détécté : Type : Trojan RAT : Daodan, W32.Bratle, W32.Zotob, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 3333")

            writer.Close()

        End If

        If port = "65529" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65529 Détécté : Type : Trojan VER : W32.SPYBOT, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 65529")

            writer.Close()
        End If

        If destinationport = "65529" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65529 Détécté : Type : Trojan VER : W32.SPYBOT, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 65529")

            writer.Close()

        End If

        If port = "8076" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8076 Détécté : Type : Trojan VER : W32.SPYBOT, W32.Mytob, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 8076")

            writer.Close()
        End If

        If destinationport = "8076" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8076 Détécté : Type : Trojan VER : W32.SPYBOT, W32.Mytob, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 8076")

            writer.Close()

        End If

        If port = "44449" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 44449 Détécté : Type : Trojan : Reserver par l'iana : se port n'est pas autorisé et il s'agit donc d'un trojan, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 44449 (UNAUTHORIZED USE)")

            writer.Close()
        End If

        If destinationport = "44449" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 44449 Détécté : Type : Trojan : Reserver par l'iana : se port n'est pas autorisé et il s'agit donc d'un trojan, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 44449 (UNAUTHORIZED USE)")

            writer.Close()

        End If

        If port = "24034" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 24034 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 24034 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "24034" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 24034 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 24034 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "11620" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 11620 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 11620 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "11620" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 11620 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 11620 (Reserved by IANA)")

            writer.Close()

        End If


        If port = "56885" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 56885 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 56885 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "56885" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 56885 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 56885 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "4500" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4500 Détécté : Type : DDOS, 905/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 4500")

            writer.Close()
        End If

        If destinationport = "4500" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4500 Détécté : Type : DDOS, 9.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 4500")

            writer.Close()

        End If

        If port = "59889" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 59889 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 59889 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "59889" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 59889 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 59889 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "52651" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52651 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52651 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "52651" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52651 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52651 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "63339" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 63339 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 63339 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "63339" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 63339 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 63339 (Reserved by IANA)")

            writer.Close()

        End If
        If port = "63338" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 63338 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 63338 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "63338" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 63338 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 63338 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "52803" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52803 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52803 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "52803" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52803 Détécté : Type : malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52803 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "51" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 51 Détécté : Type : Backdoor, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 51")

            writer.Close()
        End If

        If destinationport = "51" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 51 Détécté : Type : Backdoor, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 51")

            writer.Close()

        End If

        If port = "52" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52 Détécté : Type : Trojan : Skun, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52")

            writer.Close()
        End If

        If destinationport = "52" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52 Détécté : Type : Trojan : Skun, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 52")

            writer.Close()

        End If


        If port = "61899" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 61899 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 61899 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "61899" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 61899 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 61899 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "65355" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65355 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 65355 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "65355" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65355 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 65355 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "39642" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 39642 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 39642 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "39642" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 39642 Détécté : Type : Malveillant, unkown/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 39642 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "17500" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17500 Détécté : Type : Trojan : si vous utilisez Dropbox alors vous n'avez peut être pas de virus sinon : CrazzyNet, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17500 (Reserved by IANA)")

            writer.Close()
        End If

        If destinationport = "17500" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17500 Détécté : Type : Trojan : si vous utilisez Dropbox alors vous n'avez peut être pas de virus sinon : CrazzyNet, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17500 (Reserved by IANA)")

            writer.Close()

        End If

        If port = "40147" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 40147 Détécté : Type : Trojan : Se port n'est pas forcement malveillant mais est exploitable par des malwares, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 40147")

            writer.Close()
        End If

        If destinationport = "40147" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 40147 Détécté : Type : Trojan : Se port n'est pas forcement malveillant mais est exploitable par des malwares, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 40147")

            writer.Close()

        End If

        If port = "1011" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1011 Détécté : Type : Trojan : Doly trojan, augudor, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1011")

            writer.Close()
        End If

        If destinationport = "1011" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1011 Détécté : Type : Trojan : Doly trojan, augudor, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1011")

            writer.Close()

        End If

        If port = "54" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 54 Détécté : Type : Trojan : MuSka52, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 54")

            writer.Close()
        End If

        If destinationport = "54" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 54 Détécté : Type : Trojan : MuSka52, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 54")

            writer.Close()

        End If

        If port = "58" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 58 Détécté : Type : Trojan : DMSetup trojan, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 58")

            writer.Close()
        End If

        If destinationport = "58" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 58 Détécté : Type : Trojan : DMSetup trojan, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 58")

            writer.Close()

        End If


        If port = "48" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 48 Détécté : Type : Trojan RAT: DRAT, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 48")

            writer.Close()
        End If

        If destinationport = "48" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 48 Détécté : Type : Trojan RAT: DRAT, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 48")

            writer.Close()

        End If

        If port = "1" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1 Détécté : Type : Trojan : Sockets des Troie, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1")

            writer.Close()
        End If

        If destinationport = "1" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1 Détécté : Type : Trojan : Sockets des Troie, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1")

            writer.Close()

        End If

        If port = "1036" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1036 Détécté : Type : Trojan : KWM, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1036")

            writer.Close()
        End If

        If destinationport = "1036" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1036 Détécté : Type : Trojan : KWM, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 1036")

            writer.Close()

        End If

        If port = "30" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30 Détécté : Type : Trojan : Agent 40421, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 30")

            writer.Close()
        End If

        If destinationport = "30" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30 Détécté : Type : Trojan : Agent 40421, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 30")

            writer.Close()

        End If

        If port = "29695" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 29695 Détécté : Type : Malveillant, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 29695")

            writer.Close()
        End If

        If destinationport = "29695" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 29695 Détécté : Type : Malveillant, 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 29695")

            writer.Close()

        End If

        If port = "5501" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5501 Détécté : Type : Malveillant, SecurID ACE/Server Slave 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5501")

            writer.Close()
        End If

        If destinationport = "5501" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 5501 Détécté : Type : Malveillant, SecurID ACE/Server Slave 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 5501")

            writer.Close()

        End If
        If port = "201" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 201 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 201")

            writer.Close()
        End If

        If destinationport = "201" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 201 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 201")

            writer.Close()

        End If

        If port = "202" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 202 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 202")

            writer.Close()
        End If

        If destinationport = "202" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 202 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 202")

            writer.Close()

        End If

        If port = "203" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 203 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 203")

            writer.Close()
        End If

        If destinationport = "203" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 203 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 203")

            writer.Close()

        End If

        If port = "204" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 204 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 204")

            writer.Close()
        End If

        If destinationport = "204" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 204 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 204")

            writer.Close()

        End If

        If port = "205" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 205 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 205")

            writer.Close()
        End If

        If destinationport = "205" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 205 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 205")

            writer.Close()

        End If

        If port = "206" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 206 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 206")

            writer.Close()
        End If

        If destinationport = "206" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 206 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 206")

            writer.Close()

        End If

        If port = "207" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 207 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 207")

            writer.Close()
        End If

        If destinationport = "207" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 207 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 207")

            writer.Close()

        End If

        If port = "208" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 208 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 208")

            writer.Close()
        End If

        If destinationport = "208" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 208 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 208")

            writer.Close()

        End If

        If port = "17585" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17585 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17585")

            writer.Close()
        End If

        If destinationport = "17585" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17585 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17585")

            writer.Close()

        End If

        If port = "17585" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17585 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17585")

            writer.Close()
        End If

        If destinationport = "17585" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 17585 Détécté : Type : Trojan : One Windows Trojan 7.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 17585")

            writer.Close()

        End If

        If port = "119" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 119 Détécté : Type : Trojan : Happy99 (a.k.a. Ska trojan), Horrortel, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 119")

            writer.Close()
        End If

        If destinationport = "119" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 119 Détécté : Type : Trojan : Happy99 (a.k.a. Ska trojan), Horrortel, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 119")

            writer.Close()

        End If

        If port = "55000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 55000 Détécté : Type : Backdoor : Roxe, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 55000")

            writer.Close()
        End If

        If destinationport = "55000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 55000 Détécté : Type : Backdoor : Roxe, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 55000")

            writer.Close()

        End If

        If port = "30000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30000 Détécté : Type : DataRape, Infector, 9.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 30000")

            writer.Close()
        End If

        If destinationport = "30000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30000 Détécté : Type : DataRape, Infector, 9.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 30000")

            writer.Close()
        End If

        If port = "6666" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 6666 Détécté : Type : Trojan AL-Bareki, KiLo, SpArTa, Dark Connection Inside, Dark Connection, NetBus worm, TCPShell.c, AltaVista Tunnel server, 	BAT.Boohoo.Worm, napster, Script kiddies trying to compromise Real Server, Foobot, W32.HLLW.Warpigs, Kali uses UDP 6666., 9.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 6666")

            writer.Close()
        End If

        If destinationport = "6666" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 6666 Détécté : Type : Trojan AL-Bareki, KiLo, SpArTa, Dark Connection Inside, Dark Connection, NetBus worm, TCPShell.c, AltaVista Tunnel server, 	BAT.Boohoo.Worm, napster, Script kiddies trying to compromise Real Server, Foobot, W32.HLLW.Warpigs, Kali uses UDP 6666., 9.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 6666")

            writer.Close()

        End If

        If port = "4444" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4444 Détécté  Type  Trojan RAT CrackDown, Oracle, Prosiak, Swift Remote, 	Napster, W32.Blaster.Worm, W32.HLLW.Donk, W32.Mockbot, W32.Reidana, Metasploit use this port,  9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 4444")

            writer.Close()
        End If

        If destinationport = "4444" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 4444 Détécté  Type  Trojan RAT CrackDown, Oracle, Prosiak, Swift Remote, 	Napster, W32.Blaster.Worm, W32.HLLW.Donk, W32.Mockbot, W32.Reidana, Metasploit use this port,  9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write("port 4444")

            writer.Close()

        End If

        If port = "44" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 44 Détécté  Type  Trojan artic, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "44" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 44 Détécté  Type  Trojan artic, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "9778" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 9778 Détécté  Type  Backdoor, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "9778" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 9778 Détécté  Type  Backdoor, 6/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "7312" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 7312 Détécté  Type  trojan Yajing trojan, 7/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "7312" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 7312 Détécté  Type  trojan Yajing trojan, 7/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "53" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 53 Détécté  Type  trojan ADM worm, li0n, MscanWorm, MuSka52, Civcat, Esteems, W32.Dasher, W32.Spybot , 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "53" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 53 Détécté  Type  trojan ADM worm, li0n, MscanWorm, MuSka52, Civcat, Esteems, W32.Dasher, W32.Spybot , 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "257" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 257 Détécté  Type  trojanFW1 logging , 7/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "257" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 257 Détécté  Type  trojanFW1 logging , 7/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "52" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52 Détécté  Type  trojan MuSka52, Skun, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "52" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 52 Détécté  Type  trojan MuSka52, Skun, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "9000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 9000 Détécté  Type  trojan Netministrator, W32.Esbot, W32.Mytob, W32.Randex, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "9000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 9000 Détécté  Type  trojan Netministrator, W32.Esbot, W32.Mytob, W32.Randex, 9/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "8890" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8890 Détécté  Type  trojan Sendmail Switch, 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "8890" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 9000 Détécté  Type  trojanSendmail Switch, 5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "514" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 514 Détécté  Type  trojan ADM worm, RPC Backdoor, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "514" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 514 Détécté  Type  trojan ADM worm, RPC Backdoor, 8/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "30000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30000 Détécté  Type  trojan DataRape, Infector, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "30000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 30000 Détécté  Type  trojan DataRape, Infector, 8.5/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "65000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65000 Détécté  Type  trojan et DDoS, Devil trojan horse 1.03, Devil, Sockets des Troie, Stacheldraht, Stacheldraht agent - handler, 	Roxrat, 10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "65000" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 65000 Détécté  Type  trojan et DDoS, Devil trojan horse 1.03, Devil, Sockets des Troie, Stacheldraht, Stacheldraht agent - handler, 	Roxrat, 10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "3128" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3128 Détécté  Type  trojan Reverse WWW Tunnel Backdoor, RingZero, W32.HLLW.Deadhat, Squid,10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "3128" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 3128 Détécté  Type  trojan Reverse WWW Tunnel Backdoor, RingZero, W32.HLLW.Deadhat, Squid,10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "8732" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8732 Détécté  Type  trojan Kryptonic Ghost Command Pro trojan, Squid,10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "8732" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8732 Détécté  Type  trojan Kryptonic Ghost Command Pro trojan, Squid,10/10 Solution : Bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "502" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 502 Détécté  Type  suspect, 3/10 Solution : Surveillez le port"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "502" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 502 Détécté  Type  suspect, 3/10 Solution : Surveillez le port"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "70" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 70 Détécté  Type  malware, W32.Evala.Worm, ADM worm 6/10 Solution : bloquez lz port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "70" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 70 Détécté  Type  malware, W32.Evala.Worm, ADM worm 6/10 Solution : bloquez lz port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "1208" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1208 Détécté  Type  trojan, Infector 8/10 Solution : bloquez lz port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "1208" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1208 Détécté  Type  trojan, Infector 8/10 Solution : bloquez lz port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If port = "1024" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1024 Detecte  Type  trojan, Ptakks, W32.Mydoom, Jade, Latinus, Netspy, Randex, Remote Administration Tool ( RAT )  9.5/10 Solution : bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        If destinationport = "1024" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 1024 Detecte  Type  trojan, Ptakks, W32.Mydoom, Jade, Latinus, Netspy, Randex, Remote Administration Tool ( RAT )  9.5/10 Solution : bloquez le port via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
        End If

        'Vulnérabilité

        If port = "8009" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8009 Détécté  Type  Vulnerability"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            vuln = vuln + 1
            Label64.Text = "nombre de potentiel vulnérabilité : " & vuln
        End If

        If destinationport = "8009" Then
            RichTextBox1.Text = RichTextBox1.Text + "port 8009 Détécté  Type  Vulnerability"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            vuln = vuln + 1
            Label64.Text = "nombre de potentiel vulnérabilité : " & vuln
        End If

        'IP


        If ipfrom.ToString() = "239.255.255.250" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 239.255.255.250 Détécté  Type  Malveillante , 7/10 Solution : Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "239.255.255.250" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 239.255.255.250 Détécté  Type  Malveillante , 7/10 Solution : Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If


        If ipfrom.ToString() = "216.58.213.142" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 216.58.213.142 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "216.58.213.142" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 216.58.213.142 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipfrom.ToString() = "104.21.79.161" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 104.21.79.161 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "104.21.79.161" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 104.21.79.161 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipfrom.ToString() = "224.0.0.251" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 224.0.0.251 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "224.0.0.251" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 224.0.0.251 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If
        If ipfrom.ToString() = "239.255.255.250" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 239.255.255.250 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "239.255.255.250" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 239.255.255.250 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If
        If ipfrom.ToString() = "8.253.93.248" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 8.253.93.248 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        If ipto.ToString() = "8.253.93.248" Then
            RichTextBox1.Text = RichTextBox1.Text + "IP 8.253.93.248 Détécté  Type  Malveillante , 8/10 Solution :  Bloquez l'IP via le parfeu"
            detect.Visible = True
            RichTextBox1.Text = RichTextBox1.Text + Global.Microsoft.VisualBasic.ChrW(10)
            suspectip = suspectip + 1
            Label63.Text = "nombre d'IP suspecte : " & suspectip.ToString()
        End If

        safe = safe + 1

        Label66.Text = "nombre de packet normal : " & safe.ToString()

    End Sub

    Private Function Byteswap(ByVal bytez() As Byte, ByVal index As UInteger)
        Dim result(1) As Byte
        result(0) = bytez(index + 1)
        result(1) = bytez(index)
        Return result
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If started = True Then
            Button1.Text = "Start"
            started = False
        Else
            Button1.Text = "Stop"
            started = True
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If formloaded = True Then
            DGV.Size = Me.size - sizediff
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        Try
            If TextBox1.Text <> "" And TextBox1.Text IsNot Nothing Then
                FilterIPAddress = IPAddress.Parse(TextBox1.Text)
                FilterIP = True
                TextBox1.BackColor = Color.LimeGreen
            Else
                FilterIP = False
                TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
            End If
        Catch ex As Exception
            FilterIP = False
            TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        suspectpacket = 0
        suspectip = 0

        Try
            If socketexist = True Then
                started = False
                socketz.Close()
                socketexist = False

            End If


        Catch ex As Exception
            MessageBox.Show("Erreur " & ex.ToString())
        End Try


        For i = 0 To mycomputerconnections(ComboBox1.SelectedIndex).GetIPProperties.UnicastAddresses.Count - 1

            If mycomputerconnections(ComboBox1.SelectedIndex).GetIPProperties.UnicastAddresses(i).Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then

                myip = mycomputerconnections(ComboBox1.SelectedIndex).GetIPProperties.UnicastAddresses(i).Address

                yourip = myip

                BindSocket()

            End If

        Next

    End Sub

    Private Sub BindSocket()

        Try
            socketz.Bind(New IPEndPoint(myip, 0))
            socketz.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, True)
            Dim bytrue() As Byte = {1, 0, 0, 0}
            Dim byout() As Byte = {1, 0, 0, 0}
            socketz.IOControl(IOControlCode.ReceiveAll, bytrue, byout)
            socketz.Blocking = False

            ReDim bytedata(socketz.ReceiveBufferSize)
            socketz.BeginReceive(bytedata, 0, bytedata.Length, SocketFlags.None, New AsyncCallback(AddressOf OnReceive), Nothing)

            ComboBox1.Enabled = False

            Try
                Label76.Text = "Adresse IPV4 : " & socketz.AddressFamily
                Label77.Text = "Connecté à un hote distant : " & socketz.Connected
            Catch

            End Try
        Catch ex As Exception
            ComboBox1.BackColor = Color.Red
            Console.WriteLine(" " & ex.ToString())
            MessageBox.Show("Erreur " & ex.ToString())
        End Try

    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick



        Const NUMBER_OF_ROWS_TO_ALLOW As Integer = 80

        DGV.AllowUserToAddRows = DGV.RowCount <= NUMBER_OF_ROWS_TO_ALLOW

        DGV.AllowUserToAddRows = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Application.Restart()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try

            ProgressBar1.Value = ProgressBar1.Value + 50
            TextBox2.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être au dessus de 999999999 !                     ", "error !")

        End Try



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            ProgressBar1.Value = ProgressBar1.Value - 50
            TextBox2.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être négative !                     ", "error !")
        End Try

    End Sub

    Private Sub ActualiserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualiserToolStripMenuItem.Click, ToolStripMenuItem4.Click
        Const NUMBER_OF_ROWS_TO_ALLOW As Integer = 80

        DGV.AllowUserToAddRows = DGV.RowCount <= NUMBER_OF_ROWS_TO_ALLOW

        DGV.AllowUserToAddRows = False

        If started = True Then

        Else
            started = True
            Timer2.Start()
        End If


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        started = False
        Timer2.Stop()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        Dim writer As TextWriter = New StreamWriter("résultscan.log")

        writer.Write(RichTextBox1.Text)

        writer.Close()
    End Sub

    Private Sub CollerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollerToolStripMenuItem.Click, ToolStripMenuItem10.Click

    End Sub

    Private Sub CopierToolStripButton_Click(sender As Object, e As EventArgs) Handles CopierToolStripButton.Click
        RichTextBox1.Copy()
    End Sub

    Private Sub CollerToolStripButton_Click(sender As Object, e As EventArgs) Handles CollerToolStripButton.Click
        RichTextBox1.Paste()
    End Sub

    Private Sub QuitterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitterToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage6_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub OptionsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem2.Click
        options.Visible = True
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Timer4.Start()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Timer4.Stop()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        RichTextBox2.Clear()
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick


        Try
            If destinationport = TextBox5.Text Then
                RichTextBox2.Text = RichTextBox2.Text + "port : " & port & " destination port : " & destinationport & "  Ipfrom : " & ipfrom.ToString() & "  Ipfrom Family: " & ipfrom.AddressFamily.InterNetwork.ToString() & "  Ipfrom Family V6: " & ipfrom.AddressFamily.InterNetworkV6.ToString() & "  ipto : " & ipto.ToString() & "  ipto Family: " & "ip local" & myip.ToString() & ipto.AddressFamily.InterNetwork.ToString() & "  ipto Family V6 : " & ipto.AddressFamily.InterNetworkV6.ToString() & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ""
            End If

        Finally
        End Try

        Try

            If port = TextBox4.Text Then
                RichTextBox2.Text = RichTextBox2.Text + "port : " & port & " destination port : " & destinationport & "  Ipfrom : " & ipfrom.ToString() & "  Ipfrom Family: " & ipfrom.AddressFamily.InterNetwork.ToString() & "  Ipfrom Family V6: " & ipfrom.AddressFamily.InterNetworkV6.ToString() & "  ipto : " & ipto.ToString() & "  ipto Family: " & "ip local" & myip.ToString() & ipto.AddressFamily.InterNetwork.ToString() & "  ipto Family V6 : " & ipto.AddressFamily.InterNetworkV6.ToString() & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ""
            End If

        Finally
        End Try
    End Sub

    Private Sub Panel14_Paint(sender As Object, e As PaintEventArgs) Handles Panel14.Paint

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub NouveauToolStripButton_Click(sender As Object, e As EventArgs) Handles NouveauToolStripButton.Click
        RichTextBox1.Clear()
        Try
            suspectpacket = suspectpacket - 1
            Label62.Text = "nombre de packet suspect : " & suspectpacket
        Catch ex As Exception

        End Try

        If (suspectpacket < 0) Then
            suspectpacket = suspectpacket + 1
        End If
    End Sub

    Private Sub CreatorInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreatorInformationToolStripMenuItem.Click
        creator.Visible = True
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub SommaireToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SommaireToolStripMenuItem.Click
        info.Visible = True
    End Sub

    Private Sub IndexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IndexToolStripMenuItem.Click
        MessageBox.Show("Error 404 unkown                                     ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub RechercherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RechercherToolStripMenuItem.Click
        MessageBox.Show("Error 404 unkown                                     ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Form2.Visible = True
    End Sub

    Dim p1 As Process
    Dim p2 As Process
    Dim p3 As Process
    Dim p4 As Process
    Dim p5 As Process
    Dim p6 As Process
    Dim p7 As Process

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        ListBox1.Items.Clear()
        Button17.Enabled = False
        Button18.Enabled = True
        Label22.Text = "Waiting"

        ListBox1.Items.Add("---------------------------------------------------------------------------------------")
        ListBox1.Items.Add("Connection local : ")
        ListBox1.Items.Add("")

        Label22.Text = "Verifing local host"

        BackgroundWorker1.WorkerReportsProgress = True
        BackgroundWorker1.WorkerSupportsCancellation = True
        Dim activeConnection() As System.Net.NetworkInformation.TcpConnectionInformation = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties.GetActiveTcpConnections
        Dim portList As New ArrayList
        For Each conn As System.Net.NetworkInformation.TcpConnectionInformation In activeConnection
            ListBox1.Items.Add("Local :  " & "Port : " & conn.LocalEndPoint.Port & "        IP : " & conn.LocalEndPoint.Address.ToString() & "         IP famille : " & conn.LocalEndPoint.AddressFamily.ToString() & " : " & conn.LocalEndPoint.AddressFamily & "      IsMapped : " &
                               conn.LocalEndPoint.Address.IsIPv4MappedToIPv6 & "         is local IPV6 : " & conn.LocalEndPoint.Address.IsIPv6LinkLocal)
        Next

        Label22.Text = "Verifing remote host"

        ListBox1.Items.Add("---------------------------------------------------------------------------------------")
        ListBox1.Items.Add("Connection distante : ")
        ListBox1.Items.Add("")

        For Each conn As System.Net.NetworkInformation.TcpConnectionInformation In activeConnection
            ListBox1.Items.Add("distante :  " & "Port : " & conn.RemoteEndPoint.Port & "        IP : " & conn.RemoteEndPoint.Address.ToString() & "         IP famille : " & conn.RemoteEndPoint.AddressFamily.ToString() & " : " & conn.RemoteEndPoint.AddressFamily & "      IsMapped : " &
                               conn.RemoteEndPoint.Address.IsIPv4MappedToIPv6 & "         is local IPV6 : " & conn.RemoteEndPoint.Address.IsIPv6LinkLocal)
        Next

        Label22.Text = "Verifing other informations"
        ListBox1.Items.Add("---------------------------------------------------------------------------------------")
        ListBox1.Items.Add("Autre information : ")
        ListBox1.Items.Add("")

        For Each conn As System.Net.NetworkInformation.TcpConnectionInformation In activeConnection
            ListBox1.Items.Add("Nombre de connection active : " & activeConnection.Length & "    la taille est telle fixée ? : " & activeConnection.IsFixedSize & "     lecture seul ? : " & activeConnection.IsReadOnly & "  Synchronisée ? : " & activeConnection.IsSynchronized & "   Rank : " & activeConnection.Rank)
        Next



        Button18.Enabled = False
        Button17.Enabled = True

        Label22.Text = "Waiting"




        netstatanof.Start()
    End Sub


    Dim Counter As Integer = 0
    Dim SB As StringBuilder

    Private Sub netstatanof_Tick(sender As Object, e As EventArgs) Handles netstatanof.Tick




    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        BackgroundWorker1.CancelAsync()
        Button18.Enabled = False
        Button17.Enabled = True
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            SB = New StringBuilder


            SB.Append(Now.ToString & vbCrLf & vbCrLf)

            Dim NetstatInfo1 As New ProcessStartInfo
            NetstatInfo1.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo1.Arguments = ChrW(34) & "-a" & ChrW(34)
            NetstatInfo1.CreateNoWindow = True
            NetstatInfo1.UseShellExecute = False
            NetstatInfo1.RedirectStandardOutput = True
            p1 = Process.Start(NetstatInfo1)
            SB.Append(p1.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(1)



            Dim NetstatInfo2 As New ProcessStartInfo
            NetstatInfo2.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo2.Arguments = ChrW(34) & "-e" & ChrW(34)
            NetstatInfo2.CreateNoWindow = True
            NetstatInfo2.UseShellExecute = False
            NetstatInfo2.RedirectStandardOutput = True
            p2 = Process.Start(NetstatInfo2)
            SB.Append(p2.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(2)



            Dim NetstatInfo3 As New ProcessStartInfo
            NetstatInfo3.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo3.Arguments = ChrW(34) & "-n" & ChrW(34)
            NetstatInfo3.CreateNoWindow = True
            NetstatInfo3.UseShellExecute = False
            NetstatInfo3.RedirectStandardOutput = True
            p3 = Process.Start(NetstatInfo3)
            SB.Append(p3.StandardOutput.ReadToEnd)
            Counter += 1
            BackgroundWorker1.ReportProgress(3)



            Dim NetstatInfo4 As New ProcessStartInfo
            NetstatInfo4.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo4.Arguments = ChrW(34) & "-o" & ChrW(34)
            NetstatInfo4.CreateNoWindow = True
            NetstatInfo4.UseShellExecute = False
            NetstatInfo4.RedirectStandardOutput = True
            p4 = Process.Start(NetstatInfo4)
            SB.Append(p4.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(4)



            Dim NetstatInfo5 As New ProcessStartInfo
            NetstatInfo5.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo5.Arguments = ChrW(34) & "-p" & ChrW(34)
            NetstatInfo5.CreateNoWindow = True
            NetstatInfo5.UseShellExecute = False
            NetstatInfo5.RedirectStandardOutput = True
            p5 = Process.Start(NetstatInfo5)
            SB.Append(p5.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(5)



            Dim NetstatInfo6 As New ProcessStartInfo
            NetstatInfo6.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo6.Arguments = ChrW(34) & "-r" & ChrW(34)
            NetstatInfo6.CreateNoWindow = True
            NetstatInfo6.UseShellExecute = False
            NetstatInfo6.RedirectStandardOutput = True
            p6 = Process.Start(NetstatInfo6)
            SB.Append(p6.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(6)



            Dim NetstatInfo7 As New ProcessStartInfo
            NetstatInfo7.FileName = "C:\Windows\System32\Netstat.exe"
            NetstatInfo7.Arguments = ChrW(34) & "-s" & ChrW(34)
            NetstatInfo7.CreateNoWindow = True
            NetstatInfo7.UseShellExecute = False
            NetstatInfo7.RedirectStandardOutput = True
            p7 = Process.Start(NetstatInfo7)
            SB.Append(p7.StandardOutput.ReadToEnd)
            SB.Append(vbCrLf & "_________________" & vbCrLf)
            Counter += 1
            BackgroundWorker1.ReportProgress(7)



            Counter = 8
            BackgroundWorker1.ReportProgress(8)


            Counter = 0
        Catch
        End Try

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try
            If e.ProgressPercentage = 1 Then Label22.Text = "Netstat -a is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 2 Then Label22.Text = "Netstat -e is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 3 Then Label22.Text = "Netstat -n is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 4 Then Label22.Text = "Netstat -o is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 5 Then Label22.Text = "Netstat -p is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 6 Then Label22.Text = "Netstat -r is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 7 Then Label22.Text = "Netstat -s is running"
        Catch
        End Try

        Try
            If e.ProgressPercentage = 8 Then Label22.Text = "Waiting"
        Catch
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        RichTextBox4.Text = SB.ToString
        Button18.Enabled = False
        Button17.Enabled = True
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Try
            Dim activeConnection() As System.Net.NetworkInformation.TcpConnectionInformation = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties.GetActiveTcpConnections
            Dim portList As New ArrayList
            For Each conn As System.Net.NetworkInformation.TcpConnectionInformation In activeConnection
                TextBox12.Text = activeConnection.Length
            Next
        Catch
        End Try
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Label28.Text = "Oui"
        Try
            File.Create("autoclear.conf")
        Catch ex As Exception

        End Try
        autoclear.Start()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Label28.Text = "Non"
        Try
            File.Delete("autoclear.conf")
        Catch ex As Exception

        End Try
        autoclear.Stop()
    End Sub

    Private Sub autoclear_Tick(sender As Object, e As EventArgs) Handles autoclear.Tick
        RichTextBox1.Clear()
        Try
            suspectpacket = suspectpacket - 1
            Label62.Text = "nombre de packet suspect : " & suspectpacket
        Catch ex As Exception

        End Try

        If (suspectpacket < 0) Then
            suspectpacket = suspectpacket + 1
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click

        ProgressBar1.Value = 0
        ProgressBar2.Value = 0

        If System.IO.File.Exists("C:\WINDOWS\system32\csrsc.exe") Then
            RichTextBox5.Text = RichTextBox5.Text + "W32.SPYBOT : C:\WINDOWS\system32\csrsc.exe"
            ProgressBar3.Value = ProgressBar3.Value + 1
            Label29.Text = ProgressBar3.Value
        End If
        ProgressBar2.Value = ProgressBar2.Value + 1
        Label19.Text = ProgressBar2.Value.ToString

        If System.IO.File.Exists("C:\WINDOWS\system32\x.exe") Then
            RichTextBox5.Text = RichTextBox5.Text + "W32.SPYBOT : C:\WINDOWS\system32\x.exe"
            ProgressBar3.Value = ProgressBar3.Value + 1
            Label29.Text = ProgressBar3.Value
        End If
        ProgressBar2.Value = ProgressBar2.Value + 1
        Label19.Text = ProgressBar2.Value.ToString

        If System.IO.File.Exists("C:\WINDOWS\system32\x.exe") Then
            RichTextBox5.Text = RichTextBox5.Text + "W32.SPYBOT : C:\WINDOWS\system32\x.exe"
            ProgressBar3.Value = ProgressBar3.Value + 1
            Label29.Text = ProgressBar3.Value
        End If
        ProgressBar2.Value = ProgressBar2.Value + 1
        Label19.Text = ProgressBar2.Value.ToString
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click

        Dim writer As TextWriter = New StreamWriter("givehistory.bat")

        writer.Write("ipconfig /displaydns | findstr  " + TextBox13.Text + " > result.txt")

        writer.Close()


        Process.Start("givehistory.bat")

        Timer6.Start()

    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick

        Try
            RichTextBox6.Clear()
            RichTextBox6.LoadFile("result.txt", RichTextBoxStreamType.PlainText)

        Catch

        End Try

        Timer6.Stop()

    End Sub


    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click



        Dim writer As TextWriter = New StreamWriter("flushDNS.bat")

        writer.Write("ipconfig /flushdns")

        writer.Close()

        Process.Start("flushDNS.bat")

        RichTextBox6.Clear()
        RichTextBox6.Text = ("Cache DNS supprimer !")

    End Sub


    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click

        Timer7.Start()

    End Sub


    Private Sub RapideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RapideToolStripMenuItem.Click

        Timer7.Interval = 800

    End Sub


    Private Sub TrésRapideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrésRapideToolStripMenuItem.Click

        Timer7.Interval = 500

    End Sub


    Private Sub EnTempDirectPeutConsommerBeaucoupDeRAMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnTempDirectPeutConsommerBeaucoupDeRAMToolStripMenuItem.Click

        Timer7.Interval = 1

    End Sub


    Private Sub NormalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalToolStripMenuItem.Click

        Timer7.Interval = 1000

    End Sub


    Private Sub MoyenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoyenToolStripMenuItem.Click

        Timer7.Interval = 1200

    End Sub


    Private Sub LentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LentToolStripMenuItem.Click

        Timer7.Interval = 2000

    End Sub


    Private Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click

        ToolStripTextBox2.Enabled = False
        NiveauDactualisationToolStripMenuItem.Enabled = False
        StartToolStripMenuItem.Enabled = False
        StopToolStripMenuItem.Enabled = True
        PauseToolStripMenuItem.Enabled = True
        Timer7.Start()

    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick



        Try
            If destinationport = ToolStripTextBox2.Text Then
                DGV3.Rows.Add()
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(0).Value = port
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(1).Value = destinationport
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(2).Value = ipfrom.ToString 'From Column, size at 125
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(3).Value = ipto.ToString  'To Column, size at 125
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(4).Value = Typez 'Type Column, size at 50
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(5).Value = stringz 'Data column, size mode set to fill
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(6).Value = cryptstringz 'Data column, size mode set to fill
            End If

        Finally

        End Try

        Try
            If port = ToolStripTextBox2.Text Then
                DGV3.Rows.Add()
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(0).Value = TimeOfDay.Hour & "H " & TimeOfDay.Minute & "M " & TimeOfDay.Second & "S"
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(1).Value = port
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(2).Value = destinationport
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(3).Value = ipfrom.ToString 'From Column, size at 125
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(4).Value = ipto.ToString  'To Column, size at 125
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(5).Value = Typez 'Type Column, size at 50
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(6).Value = stringz 'Data column, size mode set to fill
                DGV3.Rows(DGV3.Rows.Count - 1).Cells(7).Value = cryptstringz 'Data column, size mode set to fill
            End If

        Finally
        End Try



    End Sub



    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click

        Timer7.Stop()
        ToolStripTextBox2.Enabled = True
        NiveauDactualisationToolStripMenuItem.Enabled = True
        StartToolStripMenuItem.Enabled = True
        StopToolStripMenuItem.Enabled = False
        PauseToolStripMenuItem.Enabled = False

    End Sub

    Private Sub PauseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PauseToolStripMenuItem.Click

        Timer7.Stop()
        StartToolStripMenuItem.Enabled = True
        StopToolStripMenuItem.Enabled = True
        PauseToolStripMenuItem.Enabled = False

    End Sub

    Private Sub SupprimerToutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToutToolStripMenuItem.Click

        DGV3.Rows.Clear()

    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click

        Panel1.BackColor = Color.White
        Panel1.ForeColor = Color.Black

    End Sub


    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click

        Panel1.BackColor = Color.FromArgb(64, 64, 64)
        Panel1.ForeColor = Color.White

    End Sub



    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click

        ListView1.Clear()

        Dim gy As Integer

        ListView1.Columns.Add("Programs", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Full Path", 320, HorizontalAlignment.Left)
        ListView1.SmallImageList = ImgList 'Uses the ImgList for the icons. 
        ListView1.FullRowSelect = True
        ListView1.View = View.Details
        ListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable
        ListView1.Items.Add("-------Applications-----------------------------")

        For Each proc As Process In Process.GetProcesses
            If proc.MainWindowTitle <> "" Then
                Try
                    ImgList.Images.Add(Icon.ExtractAssociatedIcon(proc.MainModule.FileName))
                Catch
                    Console.WriteLine("Erreur de chargement d'image" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "")
                End Try
                Dim lvi As New ListViewItem(proc.ProcessName, ImgList.Images.Count - 1)
                lvi.SubItems.Add(proc.MainWindowTitle)
                ListView1.Items.Add(lvi)
                gy = gy + 1

            End If
        Next

        ListView1.Items.Add("------------------------------------------------")

        For Each prc As Process In Process.GetProcesses

            ListView1.Items.Add(prc.ProcessName)
            Try
                ImgList.Images.Add(Icon.ExtractAssociatedIcon(prc.MainModule.FileName))
            Catch
                Console.WriteLine("Erreur de chargement d'image" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "")
            End Try
            Dim lvi As New ListViewItem(prc.ProcessName, ImgList.Images.Count - 1)
            lvi.SubItems.Add(prc.MainWindowTitle)
            ListView1.Items.Add(lvi)



            gy = ListView1.Items.Count - gy

            gy = gy - 2
            gy = gy / 2

            TextBox29.Text = gy




        Next

    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TéléchargerCode0PacketReaderToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click



    End Sub

    Public Class DomainAge

        Public Property human As String

        Public Property timestamp As Integer

        Public Property iso As DateTime

    End Class

    Public Class Root

        Public Property message As String

        Public Property success As Boolean

        Public Property unsafe As Boolean

        Public Property domain As String

        Public Property ip_address As String

        Public Property server As String

        Public Property content_type As String

        Public Property status_code As Integer

        Public Property page_size As Integer

        Public Property domain_rank As Integer

        Public Property dns_valid As Boolean

        Public Property parking As Boolean

        Public Property spamming As Boolean

        Public Property malware As Boolean

        Public Property phishing As Boolean

        Public Property suspicious As Boolean

        Public Property adult As Boolean

        Public Property risk_score As Integer

        Public Property category As String

        Public Property domain_age As DomainAge

        Public Property request_id As String

    End Class

    Class SurroundingClass

    End Class






    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        MessageBox.Show("Cette fonctionnalité n'est plus disponible.")
    End Sub



    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click

        Application.Restart()

    End Sub

    Private Sub StopToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem1.Click

        Application.Exit()

    End Sub





    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click

        RichTextBox7.Clear()

        Dim properties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Dim ipstats As IPGlobalStatistics = properties.GetIPv4GlobalStatistics()
        RichTextBox7.Text = RichTextBox7.Text & ("  Outbound Packet Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
        RichTextBox7.Text = RichTextBox7.Text & ("      Requested ........................... : {0}" & ipstats.OutputPacketRequests) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
        RichTextBox7.Text = RichTextBox7.Text & ("      Discarded ........................... : {0}" & ipstats.OutputPacketsDiscarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
        RichTextBox7.Text = RichTextBox7.Text & ("      No Routing Discards ................. : {0}" & ipstats.OutputPacketsWithNoRoute) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
        RichTextBox7.Text = RichTextBox7.Text & ("      Routing Entry Discards .............. : {0}" & ipstats.OutputPacketRoutingDiscards) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)

        Dim propertiess As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Dim ipstat As IPGlobalStatistics = Nothing

        If (NetworkInterfaceComponent.IPv4) Then

            ipstat = properties.GetIPv4GlobalStatistics()

            RichTextBox7.Text = RichTextBox7.Text & ("{0}IPv4 Statistics " & Environment.NewLine)
            RichTextBox7.Text = RichTextBox7.Text & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Forwarding enabled ...................... : {0}" &
            ipstat.ForwardingEnabled) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Interfaces .............................. : {0}" &
                ipstat.NumberOfInterfaces) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  IP addresses ............................ : {0}" &
                ipstat.NumberOfIPAddresses) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Routes .................................. : {0}" &
                ipstat.NumberOfRoutes) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Default TTL ............................. : {0}" &
                ipstat.DefaultTtl) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Inbound Packet Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Received ............................ : {0}" &
                ipstat.ReceivedPackets) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Forwarded ........................... : {0}" &
                ipstat.ReceivedPacketsForwarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Delivered ........................... : {0}" &
                ipstat.ReceivedPacketsDelivered) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Discarded ........................... : {0}" &
                ipstat.ReceivedPacketsDiscarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Header Errors ....................... : {0}" &
                ipstat.ReceivedPacketsWithHeadersErrors) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Address Errors ...................... : {0}" &
                ipstat.ReceivedPacketsWithAddressErrors) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Unknown Protocol Errors ............. : {0}" &
                ipstat.ReceivedPacketsWithUnknownProtocol) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Outbound Packet Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Requested ........................... : {0}" &
                 ipstat.OutputPacketRequests) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Discarded ........................... : {0}" &
                ipstat.OutputPacketsDiscarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      No Routing Discards ................. : {0}" &
                ipstat.OutputPacketsWithNoRoute) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Routing Entry Discards .............. : {0}" &
                ipstat.OutputPacketRoutingDiscards) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Reassembly Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Reassembly Timeout .................. : {0}" &
                ipstat.PacketReassemblyTimeout) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Reassemblies Required ............... : {0}" &
                ipstat.PacketReassembliesRequired) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Packets Reassembled ................. : {0}" &
                ipstat.PacketsReassembled) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Packets Fragmented .................. : {0}" &
                ipstat.PacketsFragmented) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Fragment Failures ................... : {0}" &
                ipstat.PacketFragmentFailures) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            Console.WriteLine("")
        End If

        If (NetworkInterfaceComponent.IPv6) Then

            ipstat = properties.GetIPv4GlobalStatistics()
            RichTextBox7.Text = RichTextBox7.Text & ("{0}IPv6 Statistics " & Environment.NewLine)

            RichTextBox7.Text = RichTextBox7.Text & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Forwarding enabled ...................... : {0}" &
            ipstat.ForwardingEnabled) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Interfaces .............................. : {0}" &
                ipstat.NumberOfInterfaces) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  IP addresses ............................ : {0}" &
                ipstat.NumberOfIPAddresses) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Routes .................................. : {0}" &
                ipstat.NumberOfRoutes) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Default TTL ............................. : {0}" &
                ipstat.DefaultTtl) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Inbound Packet Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Received ............................ : {0}" &
                ipstat.ReceivedPackets) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Forwarded ........................... : {0}" &
                ipstat.ReceivedPacketsForwarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Delivered ........................... : {0}" &
                ipstat.ReceivedPacketsDelivered) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Discarded ........................... : {0}" &
                ipstat.ReceivedPacketsDiscarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Header Errors ....................... : {0}" &
                ipstat.ReceivedPacketsWithHeadersErrors) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Address Errors ...................... : {0}" &
                ipstat.ReceivedPacketsWithAddressErrors) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Unknown Protocol Errors ............. : {0}" &
                ipstat.ReceivedPacketsWithUnknownProtocol) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Outbound Packet Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Requested ........................... : {0}" &
                 ipstat.OutputPacketRequests) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Discarded ........................... : {0}" &
                ipstat.OutputPacketsDiscarded) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      No Routing Discards ................. : {0}" &
                ipstat.OutputPacketsWithNoRoute) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Routing Entry Discards .............. : {0}" &
                ipstat.OutputPacketRoutingDiscards) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("  Reassembly Data:") & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Reassembly Timeout .................. : {0}" &
                ipstat.PacketReassemblyTimeout) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Reassemblies Required ............... : {0}" &
                ipstat.PacketReassembliesRequired) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Packets Reassembled ................. : {0}" &
                ipstat.PacketsReassembled) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Packets Fragmented .................. : {0}" &
                ipstat.PacketsFragmented) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            RichTextBox7.Text = RichTextBox7.Text & ("      Fragment Failures ................... : {0}" &
                ipstat.PacketFragmentFailures) & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            Console.WriteLine("")
        End If

        Dim ips() As IPAddress = Dns.GetHostAddresses(Dns.GetHostName)
        For Each ipa As IPAddress In ips
            If ipa.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                RichTextBox7.Text = RichTextBox7.Text & "Votre IP : " & ipa.ToString & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            End If
        Next

        Dim ipss() As IPAddress = Dns.GetHostAddresses(Dns.GetHostName)
        For Each ipas As IPAddress In ipss
            If ipas.AddressFamily = Sockets.AddressFamily.InterNetworkV6 Then
                RichTextBox7.Text = RichTextBox7.Text & "Votre IPV6 : " & ipas.ToString & Environment.NewLine & Global.Microsoft.VisualBasic.ChrW(10)
            End If
        Next



        mycomputerconnections = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces


        For i = 0 To mycomputerconnections.Length - 1
            RichTextBox7.Text = RichTextBox7.Text & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & "-------------------------------------------------------------------------------------------------------------------- " & Global.Microsoft.VisualBasic.ChrW(10) & "Name :" & Global.Microsoft.VisualBasic.ChrW(10) & (mycomputerconnections(i).Name & Global.Microsoft.VisualBasic.ChrW(10) & "Description : " & mycomputerconnections(i).Description & Global.Microsoft.VisualBasic.ChrW(10) & "Id : " & mycomputerconnections(i).Id & Global.Microsoft.VisualBasic.ChrW(10) & "Operationnel statut : " & mycomputerconnections(i).OperationalStatus & Global.Microsoft.VisualBasic.ChrW(10))

            RichTextBox7.Text = RichTextBox7.Text & Global.Microsoft.VisualBasic.ChrW(10) & "Adresse internetwork : " & Net.Sockets.AddressFamily.InterNetwork & Global.Microsoft.VisualBasic.ChrW(10) & "InternetworkV6 : " & Net.Sockets.AddressFamily.InterNetworkV6 & Global.Microsoft.VisualBasic.ChrW(10)
        Next

        Try
            File.WriteAllText(
            "result.txt",
            RichTextBox7.Text)
        Catch
        End Try



    End Sub

    Private Sub Timer8_Tick(sender As Object, e As EventArgs) Handles Timer8.Tick




    End Sub


    Public f As ULong = My.Computer.Info.AvailablePhysicalMemory


    Private Sub Timer9_Tick(sender As Object, e As EventArgs) Handles Timer9.Tick



        If My.Computer.Network.IsAvailable = True Then
            ToolStripStatusLabel2.Text = ("L'ordinateur est connecté a internet")
        Else
            ToolStripStatusLabel2.Text = ("l'ordinateur est déconnecté !")
        End If

        ToolStripStatusLabel4.Text = My.Computer.Info.AvailablePhysicalMemory & " octet de RAM disponible"

        Dim mBatteryLifePct As Integer                                                          'contient la durée de vie restante de la batterie en %
        Dim mBatteryLifeRemaining As Integer                                                    'contient la durée de vie restante de la batterie en sec

        mBatteryLifePct = SystemInformation.PowerStatus.BatteryLifePercent * 100                'recupération du pourcentage
        mBatteryLifeRemaining = SystemInformation.PowerStatus.BatteryLifeRemaining            'recupération de la durée en sec

        ToolStripStatusLabel3.Text = mBatteryLifePct.ToString() & "% de batterie ( " & mBatteryLifeRemaining.ToString() & " restant )"
        ToolStripProgressBar2.Value = mBatteryLifePct

        Try
            Dim searcher As New ManagementObjectSearcher("root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature")
            For Each queryObj As ManagementObject In searcher.Get()
                Dim temp As Double = CDbl(queryObj("CurrentTemperature"))
                temp = (temp - 2200) / 10.0


            Next
        Catch err As ManagementException

        End Try







    End Sub

    Private Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim ProcID As Integer
        ' Start the Notepad application, and store the process id.
        ProcID = Shell("NOTEPAD.EXE", AppWinStyle.NormalFocus)
        ' Activate the Notepad application.
        AppActivate(ProcID)
        ' Send the keystrokes to the Notepad application.
        My.Computer.Keyboard.SendKeys("I ", True)
        My.Computer.Keyboard.SendKeys("♥", True)
        My.Computer.Keyboard.SendKeys(" code !", True)

    End Sub

    Private Sub TabPage16_Click(sender As Object, e As EventArgs) Handles TabPage16.Click

    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click

        Dim netw As String
        netw = SystemInformation.Secure

        RichTextBox8.Text = "Username : " & SystemInformation.UserName.ToString() & Global.Microsoft.VisualBasic.ChrW(10) & "Systeme de sécurité présent : " & netw & Global.Microsoft.VisualBasic.ChrW(10) & "Debug (USER.EXE) activer : " & SystemInformation.DebugOS & Global.Microsoft.VisualBasic.ChrW(10) &
        "Zone de travaille de l'écran : " & SystemInformation.WorkingArea.ToString() & Global.Microsoft.VisualBasic.ChrW(10) & "Windows Pen computing : " & SystemInformation.PenWindows

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

        If CheckBox2.Checked = True Then
            RichTextBox9.Text = RichTextBox1.Text
            Dim writer As TextWriter = New StreamWriter("mlwnotif.log")

            writer.Write(RichTextBox9.Text)

            writer.Close()
        End If

        suspectpacket = suspectpacket + 1
        Label62.Text = "nombre de packet suspect : " & suspectpacket
        safe = safe - 1
        If (safe < 0) Then
            safe = safe + 1
        End If
        Label66.Text = "nombre de packet normal : " & safe.ToString()

    End Sub

    Private Sub print_Click(sender As Object, e As EventArgs) Handles print.Click

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FontDialog1.ShowDialog()
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        Dim mafenetre1 As Form1 = New Form1()
        mafenetre1.Show()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        If (instance = False) Then
            Application.Exit()
        End If

    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs)


        'appendoutput function outputs to a rich text box
    End Sub

    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick


        If (nbrpacket > 200) Then
            Label70.Text = "Niveau de Trafic : Dangeureux pour le système !" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.White
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 200) Then
            Label70.Text = "Niveau de Trafic : Anormal ! " & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Black
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 150) Then
            Label70.Text = "Niveau de Trafic : Plus qu'extreme !" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.DarkRed
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 100) Then
            Label70.Text = "Niveau de Trafic : Extrement Haut !" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.DarkRed
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 90) Then
            Label70.Text = "Niveau de Trafic : Extreme" & "\n" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.DarkRed
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 60) Then
            Label70.Text = "Niveau de Trafic : Très haut" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Red
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 50) Then
            Label70.Text = "Niveau de Trafic : Haut" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.OrangeRed
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 40) Then
            Label70.Text = "Niveau de Trafic : Supérieur a la normal" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Lime
            nbrpacket = 0
            Return
        End If

        If (nbrpacket > 30) Then
            Label70.Text = "Niveau de Trafic : Normal" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Green
            nbrpacket = 0
        End If
        Return

        If (nbrpacket > 5) Then
            Label70.Text = "Niveau de Trafic : Bas" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Blue
            nbrpacket = 0
            Return
        End If

        If (nbrpacket < 1) Then
            Label70.Text = "Niveau de Trafic : /" & Global.Microsoft.VisualBasic.ChrW(10) & nbrpacket.ToString() & " packet /s"
            Label70.ForeColor = Color.Gray
            nbrpacket = 0
            Return
        End If










    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Try

            ProgressBar1.Value = ProgressBar1.Value + 50
            TextBox2.Text = ProgressBar1.Value.ToString()
            TextBox33.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être au dessus de 999999999 !                     ", "error !")
            TextBox33.Text = TextBox2.Text

        End Try
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Try
            ProgressBar1.Value = ProgressBar1.Value - 50
            TextBox2.Text = ProgressBar1.Value.ToString()
            TextBox33.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être négative !                     ", "error !")
            TextBox33.Text = TextBox2.Text
        End Try
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        Try

            ProgressBar1.Value = ProgressBar1.Value * 50
            TextBox2.Text = ProgressBar1.Value.ToString()
            TextBox33.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être au dessus de 999999999 !                     ", "error !")
            TextBox33.Text = TextBox2.Text

        End Try
    End Sub

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        Try
            ProgressBar1.Value = ProgressBar1.Value / 50
            TextBox2.Text = ProgressBar1.Value.ToString()
            TextBox33.Text = ProgressBar1.Value.ToString()
        Catch
            MessageBox.Show("La valeur ne peut être négative !                     ", "error !")
            TextBox33.Text = TextBox2.Text
        End Try
    End Sub

    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        Application.Exit()
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        Application.Restart()
    End Sub

    Private Sub Panel33_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click
        instance = True
        Me.Close()
    End Sub

    Private Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub btntraceroute_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub DGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DGV.RowsAdded
        ToolStripStatusLabel5.Text = "Nombre de Ligne : " & DGV.Rows.Count.ToString()
        lignetotal = lignetotal + 1
        Label110.Text = "Nombre total de ligne  : " & DGV.Rows.Count.ToString()

    End Sub


    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Private Sub Panel33_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel33.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub Panel33_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel33.MouseMove


        If MoveForm Then
            If Me.WindowState = WindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Private Sub Panel33_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel33.MouseUp

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public R As Int64 = 250
    Public G As Int64 = 3
    Public B As Int64 = 35

    Public R1 As Boolean = True
    Public G1 As Boolean = False
    Public B1 As Boolean = False

    Private Sub Timer11_Tick(sender As Object, e As EventArgs) Handles Timer11.Tick
        Me.Label80.ForeColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
        Me.Button60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
        Me.Button61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
        Me.Panel50.BackColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
        Me.Button67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(B, Byte), Integer))
    End Sub

    Private Sub Timer12_Tick(sender As Object, e As EventArgs) Handles Timer12.Tick

    End Sub

    Private Sub Timer13_Tick(sender As Object, e As EventArgs) Handles Timer13.Tick

    End Sub

    Private Sub Timer14_Tick(sender As Object, e As EventArgs) Handles Timer14.Tick
        If (R1 = False) Then

            R = R - 1

        End If

        If (R1 = True) Then

            R = R + 1

        End If

        If (R = 0) Then

            R1 = True

        End If

        If (R = 255) Then

            R1 = False

        End If

        Label114.Text = "R :" & R.ToString()
        Me.Label114.ForeColor = System.Drawing.Color.FromArgb(CType(CType(R, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    End Sub

    Private Sub Timer15_Tick(sender As Object, e As EventArgs) Handles Timer15.Tick
        If G1 = False Then
            G = G + 1
        End If

        If G1 = True Then
            G = G - 1
        End If

        If G = 255 Then
            G1 = True
        End If

        If G = 0 Then
            G1 = False
        End If

        Label115.Text = "G :" & G.ToString()
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(G, Byte), Integer), CType(CType(0, Byte), Integer))
    End Sub

    Private Sub Timer16_Tick(sender As Object, e As EventArgs) Handles Timer16.Tick
        If B1 = False Then
            B = B + 1
        End If

        If B1 = True Then
            B = B - 1
        End If

        If B = 255 Then
            B1 = True
        End If

        If B = 0 Then
            B1 = False
        End If

        Label116.Text = "B :" & B.ToString()
        Me.Label116.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(B, Byte), Integer))
    End Sub

    Private Sub Button49_Click_1(sender As Object, e As EventArgs) Handles Button49.Click
        Application.Exit()
    End Sub

    Private Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        If Me.WindowState = Me.WindowState.Maximized Then

            Me.WindowState = Me.WindowState.Normal
            Return
        End If

        If Me.WindowState = Me.WindowState.Normal Then

            Me.WindowState = Me.WindowState.Maximized
            Try
                If Panel50.Visible = True Then
                    Panel50.Visible = False
                    Label80.Text = "Chargement en cours du launcher..."
                    StatusStrip1.Visible = False
                    NotifyIcon2.Visible = True
                    NotifyIcon1.Visible = False
                    Me.ShowInTaskbar = False
                    Me.ShowIcon = False
                End If
            Catch
            End Try

            Try
                Panel50.Visible = True
                Label80.Text = "code 0 Packet reader"
                StatusStrip1.Visible = True
                NotifyIcon1.Visible = True
                NotifyIcon2.Visible = False
                Me.ShowInTaskbar = True
                Me.ShowIcon = True
            Catch
            End Try
            Return
        End If
    End Sub

    Private Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public agrdForm As Boolean
    Public modifForm_MousePosition As Point
    Public size As Point

    Private Sub StatusStrip1_MouseDown(sender As Object, e As MouseEventArgs) Handles StatusStrip1.MouseDown

    End Sub


    Private Sub StatusStrip1_MouseMove(sender As Object, e As MouseEventArgs) Handles StatusStrip1.MouseMove

    End Sub

    Private Sub StatusStrip1_MouseUp(sender As Object, e As MouseEventArgs) Handles StatusStrip1.MouseUp

    End Sub

    Private Sub Panel34_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel34.MouseDown
        If e.Button = MouseButtons.Left Then
            agrdForm = True
            Me.Cursor = Cursors.NoMove2D
            modifForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub Panel34_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel34.MouseMove
        size = Me.size

        If agrdForm Then
            Me.size = size + (e.Location + modifForm_MousePosition)
        End If
    End Sub

    Private Sub Panel34_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel34.MouseUp
        If e.Button = MouseButtons.Left Then
            agrdForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Button62_Click(sender As Object, e As EventArgs) Handles Button62.Click
        Panel33.Visible = True
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Button63_Click(sender As Object, e As EventArgs) Handles Button63.Click
        Panel33.Visible = False
        Me.FormBorderStyle = FormBorderStyle.Sizable
    End Sub

    Private Sub TrackBar2_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar2.ValueChanged
        Label86.Text = TrackBar2.Value.ToString()
        Me.Height = TrackBar2.Value
    End Sub

    Private Sub TrackBar1_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar1.ValueChanged
        Label87.Text = TrackBar1.Value.ToString()
        Me.Width = TrackBar1.Value
    End Sub

    Private Sub TrackBar1_MouseUp(sender As Object, e As MouseEventArgs) Handles TrackBar1.MouseUp

    End Sub

    Private Sub Panel35_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel35.MouseDown
        If e.Button = MouseButtons.Left Then
            agrdForm = True
            Me.Cursor = Cursors.NoMove2D
            modifForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub Panel35_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel35.MouseMove
        size = Me.size

        If agrdForm Then

        End If
    End Sub

    Private Sub Panel35_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel35.MouseUp
        If e.Button = MouseButtons.Left Then
            agrdForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles StatusStrip1.ItemClicked

    End Sub

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        Panel48.Visible = False
    End Sub

    Private Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        Panel48.Visible = True
    End Sub

    Private Sub Timer17_Tick(sender As Object, e As EventArgs) Handles Timer17.Tick
        Try
            pckt = 0
            pckttemp = 0
        Catch

        End Try
    End Sub

    Private Sub Timer18_Tick(sender As Object, e As EventArgs) Handles Timer18.Tick

        Try
            Label105.Text = Label61.Text
            Label106.Text = "Nombre de packet durant cette state : " & pckttemp.ToString()
            Label107.Text = "record de packet /s : " & pckrecord.ToString()

            If pckttemp > pckrecord Then
                pckrecord = pckttemp
            End If

        Catch ex As Exception

        End Try

        Try

            ProgressBar5.Value = pckt

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Timer19_Tick(sender As Object, e As EventArgs) Handles Timer19.Tick
        Button60.Enabled = True


        Panel51.Visible = True
        Timer19.Stop()


    End Sub

    Private Sub Timer20_Tick(sender As Object, e As EventArgs)
        Timer19.Start()

    End Sub

    Private Sub Label80_MouseDown(sender As Object, e As MouseEventArgs) Handles Label80.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub Label80_MouseMove(sender As Object, e As MouseEventArgs) Handles Label80.MouseMove
        If MoveForm Then
            If Me.WindowState = WindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Private Sub Label80_MouseUp(sender As Object, e As MouseEventArgs) Handles Label80.MouseUp

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If MoveForm Then
            If Me.WindowState = WindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub DGV_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles DGV.ColumnAdded

    End Sub

    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        Label113.Visible = True
    End Sub

    Private Sub Button66_MouseLeave(sender As Object, e As EventArgs) Handles Button66.MouseLeave
        Label113.Visible = False
    End Sub

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        Try
            If Panel50.Visible = True Then
                Panel50.Visible = False
                Label80.Text = "Chargement en cours du launcher..."
                StatusStrip1.Visible = False
                NotifyIcon2.Visible = True
                NotifyIcon1.Visible = False
                Me.ShowInTaskbar = False
                Me.ShowIcon = False
                Return
            End If
        Catch
        End Try

        Try
            Panel50.Visible = True
            Label80.Text = "code 0 Packet reader"
            StatusStrip1.Visible = True
            NotifyIcon1.Visible = True
            NotifyIcon2.Visible = False
            Me.ShowInTaskbar = True
            Me.ShowIcon = True
        Catch
        End Try
    End Sub

    Private Sub Button68_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TrackBar5_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar5.ValueChanged
        Try
            Label123.Text = "Red (R) :  " & TrackBar5.Value.ToString()
            Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
            Me.TabPage26.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
        Catch
        End Try
    End Sub

    Private Sub TabPage26_Click(sender As Object, e As EventArgs) Handles TabPage26.Click

    End Sub

    Private Sub TrackBar4_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar4.ValueChanged
        Try
            Label122.Text = "Green (G) :  " & TrackBar4.Value.ToString()
            Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
            Me.TabPage26.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
        Catch
        End Try
    End Sub

    Private Sub trackBar3_ValueChanged(sender As Object, e As EventArgs) Handles trackBar3.ValueChanged
        Try
            Label121.Text = "Blue (B) :  " & trackBar3.Value.ToString()
            Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
            Me.TabPage26.BackColor = System.Drawing.Color.FromArgb(CType(CType(TrackBar5.Value, Byte), Integer), CType(CType(TrackBar4.Value, Byte), Integer), CType(CType(trackBar3.Value, Byte), Integer))
        Catch
        End Try
    End Sub

    Private Sub Button68_Click_1(sender As Object, e As EventArgs) Handles Button68.Click
        Try
            GestionnaireReseau.Visible = True
        Catch
        End Try
    End Sub

    Private Sub Timer20_Tick_1(sender As Object, e As EventArgs) Handles Timer20.Tick
        Try
            Dim properties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
            Dim ipstat As IPGlobalStatistics = properties.GetIPv4GlobalStatistics()


            Try
                Label69.Text = "Nombre totale de packet sortant : " & ipstat.OutputPacketRequests()
            Catch
            End Try

            Try
                Label71.Text = "Nombre totale de packet sortant refusé : " & ipstat.OutputPacketsDiscarded()
            Catch
            End Try

            Try
                Label72.Text = "packets reçus : " & ipstat.ReceivedPackets()
            Catch
            End Try

            Try
                Label78.Text = "packets reçus délivré : " & ipstat.ReceivedPacketsDelivered()
            Catch
            End Try

            Try
                Label119.Text = "packets qui ont été reçus et ignorée : " & ipstat.ReceivedPacketsDiscarded()
            Catch
            End Try

            Try
                Label124.Text = "packets transféré : " & ipstat.ReceivedPacketsForwarded()
            Catch
            End Try

            Try
                Label125.Text = "packets avec des erreur d'adresse : " & ipstat.ReceivedPacketsWithAddressErrors()
            Catch
            End Try

            Try
                Label126.Text = "packets avec des erreur d'en-tête : " & ipstat.ReceivedPacketsWithHeadersErrors()
            Catch
            End Try

            Try
                Label127.Text = "packets reçu avec un protocol inconnue : " & ipstat.ReceivedPacketsWithUnknownProtocol()
            Catch
            End Try

            Try
                Label128.Text = "packets fragmentés : " & ipstat.PacketsFragmented()
            Catch
            End Try

            Try
                Label129.Text = "packet avec une echec de fragmantation : " & ipstat.PacketFragmentFailures()
            Catch
            End Try

            Try
                Label130.Text = "packets rassemblés : " & ipstat.PacketsReassembled()
            Catch
            End Try

            Try
                Label131.Text = "packets avec un rassemblement nécessaire : " & ipstat.PacketReassembliesRequired()
            Catch
            End Try

            Try
                Label132.Text = "packets dont le réassemblage à échoué : " & ipstat.PacketReassemblyFailures()
            Catch
            End Try

            Try
                Label133.Text = "delai maximal de réassemblage : " & ipstat.PacketReassemblyTimeout() & "ms"
            Catch
            End Try

            Try
                Label134.Text = "iténaire supprimer de la table de routage : " & ipstat.OutputPacketRoutingDiscards()
            Catch
            End Try

            Try
                Label135.Text = "durée de vie d'un packet IP : " & ipstat.DefaultTtl()
            Catch
            End Try

            Try
                Label136.Text = "Packets sans déstination : " & ipstat.OutputPacketsWithNoRoute()
            Catch
            End Try

            Try
                If (ipstat.ForwardingEnabled() = True) Then
                    Label137.Text = "transfert des packets : actif"
                End If
            Catch
            End Try

            Try
                If (ipstat.ForwardingEnabled() = False) Then
                    Label137.Text = "transfert des packets : désactiver"
                End If
            Catch
            End Try

        Catch
        End Try


    End Sub

    Private Sub NouveauToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NouveauToolStripMenuItem.Click
        Try
            Application.Restart()
        Catch
            Return
        End Try
    End Sub

    Private Sub Timer21_Tick(sender As Object, e As EventArgs) Handles Timer21.Tick

        If (TextBox3.Text = "") Then

            TextBox3.Text = "Type : TCP & UDP"

            Return
        End If

        If (TextBox3.Text = "Type : TCP & UDP") Then

            TextBox3.Text = "Rejoignez notre discord : https://discord.gg/nQBaUpdcHn"

            Return
        End If

        If (TextBox3.Text = "Rejoignez notre discord : https://discord.gg/nQBaUpdcHn") Then

            TextBox3.Text = "Notre discord : https://discord.gg/nQBaUpdcHn"

            Return
        End If

        If (TextBox3.Text = "Notre discord : https://discord.gg/nQBaUpdcHn") Then

            TextBox3.Text = "Un problème ? dites le nous ici : https://discord.gg/nQBaUpdcHn"

            Return
        End If

        If (TextBox3.Text = "Un problème ? dites le nous ici : https://discord.gg/nQBaUpdcHn") Then

            TextBox3.Text = "Vous voulez des astuces ? c'est par ici : https://discord.gg/nQBaUpdcHn"

            Return
        End If

        If (TextBox3.Text = "Vous voulez des astuces ? c'est par ici : https://discord.gg/nQBaUpdcHn") Then

            TextBox3.Text = "Code0 Packet reader à été créer par... nous même... L'avantage c'est qu'il sera gratuit... Pour toute la vie !"

            Return
        End If

        If (TextBox3.Text = "Code0 Packet reader à été créer par... nous même... L'avantage c'est qu'il sera gratuit... Pour toute la vie !") Then

            TextBox3.Text = "Type : TCP & UDP"

            Return
        End If

        TextBox3.Text = "Type : TCP & UDP"

    End Sub

    Dim Cle_Registre As Microsoft.Win32.RegistryKey

    Private Sub Button45_Click_1(sender As Object, e As EventArgs) Handles Button45.Click
        Cle_Registre = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        Cle_Registre.SetValue(My.Application.Info.AssemblyName, Application.ExecutablePath)
    End Sub

    Private Sub TabPage24_Click(sender As Object, e As EventArgs) Handles TabPage24.Click

    End Sub

    Private Sub Button46_Click_1(sender As Object, e As EventArgs) Handles Button46.Click
        Cle_Registre.DeleteValue(My.Application.Info.AssemblyName)
    End Sub

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Try
            File.WriteAllText(
"result.txt",
RichTextBox7.Text)
            Process.Start("notepad.exe", "result.txt")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Panel33_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel33.Paint

    End Sub

    Private Sub Panel23_Paint(sender As Object, e As PaintEventArgs) Handles Panel23.Paint

    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

    End Sub
End Class