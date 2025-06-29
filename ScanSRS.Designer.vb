<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScanSRS
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScanSRS))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NouveauToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OuvrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.EnregistrerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnregistrersousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AperçuavantimpressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.QuitterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnnulerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RétablirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CouperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SélectionnertoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutilsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PersonnaliserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SommaireToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RechercherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ÀproposdeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Phys = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1196, 74)
        Me.Panel1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FichierToolStripMenuItem, Me.EditionToolStripMenuItem, Me.OutilsToolStripMenuItem, Me.AideToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1194, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FichierToolStripMenuItem
        '
        Me.FichierToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NouveauToolStripMenuItem, Me.OuvrirToolStripMenuItem, Me.toolStripSeparator, Me.EnregistrerToolStripMenuItem, Me.EnregistrersousToolStripMenuItem, Me.toolStripSeparator1, Me.ImprimerToolStripMenuItem, Me.AperçuavantimpressionToolStripMenuItem, Me.toolStripSeparator2, Me.QuitterToolStripMenuItem})
        Me.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem"
        Me.FichierToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.FichierToolStripMenuItem.Text = "&Fichier"
        '
        'NouveauToolStripMenuItem
        '
        Me.NouveauToolStripMenuItem.Image = CType(resources.GetObject("NouveauToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NouveauToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NouveauToolStripMenuItem.Name = "NouveauToolStripMenuItem"
        Me.NouveauToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.NouveauToolStripMenuItem.Text = "&Nouveau"
        '
        'OuvrirToolStripMenuItem
        '
        Me.OuvrirToolStripMenuItem.Image = CType(resources.GetObject("OuvrirToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OuvrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OuvrirToolStripMenuItem.Name = "OuvrirToolStripMenuItem"
        Me.OuvrirToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.OuvrirToolStripMenuItem.Text = "&Ouvrir"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 6)
        '
        'EnregistrerToolStripMenuItem
        '
        Me.EnregistrerToolStripMenuItem.Image = CType(resources.GetObject("EnregistrerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EnregistrerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.EnregistrerToolStripMenuItem.Name = "EnregistrerToolStripMenuItem"
        Me.EnregistrerToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.EnregistrerToolStripMenuItem.Text = "&Enregistrer"
        '
        'EnregistrersousToolStripMenuItem
        '
        Me.EnregistrersousToolStripMenuItem.Name = "EnregistrersousToolStripMenuItem"
        Me.EnregistrersousToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.EnregistrersousToolStripMenuItem.Text = "Enregistrer &sous"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 6)
        '
        'ImprimerToolStripMenuItem
        '
        Me.ImprimerToolStripMenuItem.Image = CType(resources.GetObject("ImprimerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ImprimerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ImprimerToolStripMenuItem.Name = "ImprimerToolStripMenuItem"
        Me.ImprimerToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.ImprimerToolStripMenuItem.Text = "&Imprimer"
        '
        'AperçuavantimpressionToolStripMenuItem
        '
        Me.AperçuavantimpressionToolStripMenuItem.Image = CType(resources.GetObject("AperçuavantimpressionToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AperçuavantimpressionToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AperçuavantimpressionToolStripMenuItem.Name = "AperçuavantimpressionToolStripMenuItem"
        Me.AperçuavantimpressionToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.AperçuavantimpressionToolStripMenuItem.Text = "Aperçu a&vant impression"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 6)
        '
        'QuitterToolStripMenuItem
        '
        Me.QuitterToolStripMenuItem.Name = "QuitterToolStripMenuItem"
        Me.QuitterToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.QuitterToolStripMenuItem.Text = "&Quitter"
        '
        'EditionToolStripMenuItem
        '
        Me.EditionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnnulerToolStripMenuItem, Me.RétablirToolStripMenuItem, Me.toolStripSeparator3, Me.CouperToolStripMenuItem, Me.CopierToolStripMenuItem, Me.CollerToolStripMenuItem, Me.toolStripSeparator4, Me.SélectionnertoutToolStripMenuItem})
        Me.EditionToolStripMenuItem.Name = "EditionToolStripMenuItem"
        Me.EditionToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.EditionToolStripMenuItem.Text = "&Edition"
        '
        'AnnulerToolStripMenuItem
        '
        Me.AnnulerToolStripMenuItem.Name = "AnnulerToolStripMenuItem"
        Me.AnnulerToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.AnnulerToolStripMenuItem.Text = "&Annuler"
        '
        'RétablirToolStripMenuItem
        '
        Me.RétablirToolStripMenuItem.Name = "RétablirToolStripMenuItem"
        Me.RétablirToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.RétablirToolStripMenuItem.Text = "&Rétablir"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 6)
        '
        'CouperToolStripMenuItem
        '
        Me.CouperToolStripMenuItem.Image = CType(resources.GetObject("CouperToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CouperToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CouperToolStripMenuItem.Name = "CouperToolStripMenuItem"
        Me.CouperToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.CouperToolStripMenuItem.Text = "&Couper"
        '
        'CopierToolStripMenuItem
        '
        Me.CopierToolStripMenuItem.Image = CType(resources.GetObject("CopierToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopierToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopierToolStripMenuItem.Name = "CopierToolStripMenuItem"
        Me.CopierToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.CopierToolStripMenuItem.Text = "Co&pier"
        '
        'CollerToolStripMenuItem
        '
        Me.CollerToolStripMenuItem.Image = CType(resources.GetObject("CollerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CollerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CollerToolStripMenuItem.Name = "CollerToolStripMenuItem"
        Me.CollerToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.CollerToolStripMenuItem.Text = "Co&ller"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 6)
        '
        'SélectionnertoutToolStripMenuItem
        '
        Me.SélectionnertoutToolStripMenuItem.Name = "SélectionnertoutToolStripMenuItem"
        Me.SélectionnertoutToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.SélectionnertoutToolStripMenuItem.Text = "Sélectio&nner tout"
        '
        'OutilsToolStripMenuItem
        '
        Me.OutilsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PersonnaliserToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.OutilsToolStripMenuItem.Name = "OutilsToolStripMenuItem"
        Me.OutilsToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.OutilsToolStripMenuItem.Text = "&Outils"
        '
        'PersonnaliserToolStripMenuItem
        '
        Me.PersonnaliserToolStripMenuItem.Name = "PersonnaliserToolStripMenuItem"
        Me.PersonnaliserToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.PersonnaliserToolStripMenuItem.Text = "&Personnaliser"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'AideToolStripMenuItem
        '
        Me.AideToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SommaireToolStripMenuItem, Me.IndexToolStripMenuItem, Me.RechercherToolStripMenuItem, Me.toolStripSeparator5, Me.ÀproposdeToolStripMenuItem})
        Me.AideToolStripMenuItem.Name = "AideToolStripMenuItem"
        Me.AideToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.AideToolStripMenuItem.Text = "&Aide"
        '
        'SommaireToolStripMenuItem
        '
        Me.SommaireToolStripMenuItem.Name = "SommaireToolStripMenuItem"
        Me.SommaireToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.SommaireToolStripMenuItem.Text = "&Sommaire"
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.IndexToolStripMenuItem.Text = "&Index"
        '
        'RechercherToolStripMenuItem
        '
        Me.RechercherToolStripMenuItem.Name = "RechercherToolStripMenuItem"
        Me.RechercherToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.RechercherToolStripMenuItem.Text = "&Rechercher"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(6, 6)
        '
        'ÀproposdeToolStripMenuItem
        '
        Me.ÀproposdeToolStripMenuItem.Name = "ÀproposdeToolStripMenuItem"
        Me.ÀproposdeToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.ÀproposdeToolStripMenuItem.Text = "À &propos de..."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(49, 42)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(392, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "port :"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 74)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1196, 503)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1188, 537)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Scanner les port internet"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ProgressBar1)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.Phys)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1188, 477)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Scanner les port de l'ordinateur ( physique )"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Phys
        '
        Me.Phys.FormattingEnabled = True
        Me.Phys.Location = New System.Drawing.Point(737, 25)
        Me.Phys.Name = "Phys"
        Me.Phys.Size = New System.Drawing.Size(443, 446)
        Me.Phys.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 35)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "scan"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 56)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(542, 29)
        Me.ProgressBar1.TabIndex = 2
        '
        'ScanSRS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1196, 577)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ScanSRS"
        Me.Text = "ScanSRS"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FichierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NouveauToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OuvrirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents EnregistrerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EnregistrersousToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ImprimerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AperçuavantimpressionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As ToolStripSeparator
    Friend WithEvents QuitterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnnulerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RétablirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator3 As ToolStripSeparator
    Friend WithEvents CouperToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CollerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As ToolStripSeparator
    Friend WithEvents SélectionnertoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OutilsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PersonnaliserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AideToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SommaireToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RechercherToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ÀproposdeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Button1 As Button
    Friend WithEvents Phys As ListBox
End Class
