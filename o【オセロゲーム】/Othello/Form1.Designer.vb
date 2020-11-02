<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBlackTurn = New System.Windows.Forms.Label()
        Me.lblBlackCount = New System.Windows.Forms.Label()
        Me.lblWhiteTurn = New System.Windows.Forms.Label()
        Me.lblWhiteCount = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.lblRedTurn = New System.Windows.Forms.Label()
        Me.lblBlueTurn = New System.Windows.Forms.Label()
        Me.lblRedCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBlueCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(13, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(542, 491)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        Me.PictureBox1.Visible = false
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.Label1.Location = New System.Drawing.Point(574, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "黒"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.Label2.Location = New System.Drawing.Point(574, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "白"
        '
        'lblBlackTurn
        '
        Me.lblBlackTurn.BackColor = System.Drawing.Color.Black
        Me.lblBlackTurn.Location = New System.Drawing.Point(576, 53)
        Me.lblBlackTurn.Name = "lblBlackTurn"
        Me.lblBlackTurn.Size = New System.Drawing.Size(109, 2)
        Me.lblBlackTurn.TabIndex = 3
        '
        'lblBlackCount
        '
        Me.lblBlackCount.AutoSize = true
        Me.lblBlackCount.Font = New System.Drawing.Font("MS UI Gothic", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblBlackCount.Location = New System.Drawing.Point(644, 28)
        Me.lblBlackCount.Name = "lblBlackCount"
        Me.lblBlackCount.Size = New System.Drawing.Size(16, 16)
        Me.lblBlackCount.TabIndex = 5
        Me.lblBlackCount.Text = "0"
        Me.lblBlackCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWhiteTurn
        '
        Me.lblWhiteTurn.BackColor = System.Drawing.Color.White
        Me.lblWhiteTurn.Location = New System.Drawing.Point(576, 113)
        Me.lblWhiteTurn.Name = "lblWhiteTurn"
        Me.lblWhiteTurn.Size = New System.Drawing.Size(109, 2)
        Me.lblWhiteTurn.TabIndex = 7
        '
        'lblWhiteCount
        '
        Me.lblWhiteCount.AutoSize = true
        Me.lblWhiteCount.Font = New System.Drawing.Font("MS UI Gothic", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblWhiteCount.Location = New System.Drawing.Point(644, 86)
        Me.lblWhiteCount.Name = "lblWhiteCount"
        Me.lblWhiteCount.Size = New System.Drawing.Size(16, 16)
        Me.lblWhiteCount.TabIndex = 8
        Me.lblWhiteCount.Text = "0"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Button1.Location = New System.Drawing.Point(578, 305)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "黒で開始"
        Me.Button1.UseVisualStyleBackColor = false
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button2.Location = New System.Drawing.Point(578, 334)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "白で開始"
        Me.Button2.UseVisualStyleBackColor = false
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.LightCoral
        Me.Button3.Location = New System.Drawing.Point(578, 363)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = "赤で開始"
        Me.Button3.UseVisualStyleBackColor = false
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button4.Location = New System.Drawing.Point(578, 392)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "青で開始"
        Me.Button4.UseVisualStyleBackColor = false
        '
        'lblRedTurn
        '
        Me.lblRedTurn.BackColor = System.Drawing.Color.Red
        Me.lblRedTurn.Location = New System.Drawing.Point(576, 167)
        Me.lblRedTurn.Name = "lblRedTurn"
        Me.lblRedTurn.Size = New System.Drawing.Size(109, 2)
        Me.lblRedTurn.TabIndex = 13
        '
        'lblBlueTurn
        '
        Me.lblBlueTurn.BackColor = System.Drawing.Color.Blue
        Me.lblBlueTurn.Location = New System.Drawing.Point(576, 227)
        Me.lblBlueTurn.Name = "lblBlueTurn"
        Me.lblBlueTurn.Size = New System.Drawing.Size(109, 2)
        Me.lblBlueTurn.TabIndex = 14
        '
        'lblRedCount
        '
        Me.lblRedCount.AutoSize = true
        Me.lblRedCount.Font = New System.Drawing.Font("MS UI Gothic", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblRedCount.Location = New System.Drawing.Point(644, 140)
        Me.lblRedCount.Name = "lblRedCount"
        Me.lblRedCount.Size = New System.Drawing.Size(16, 16)
        Me.lblRedCount.TabIndex = 15
        Me.lblRedCount.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.Label5.Location = New System.Drawing.Point(574, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 19)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "赤"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.Label6.Location = New System.Drawing.Point(574, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 19)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "青"
        '
        'lblBlueCount
        '
        Me.lblBlueCount.AutoSize = true
        Me.lblBlueCount.Font = New System.Drawing.Font("MS UI Gothic", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblBlueCount.Location = New System.Drawing.Point(644, 200)
        Me.lblBlueCount.Name = "lblBlueCount"
        Me.lblBlueCount.Size = New System.Drawing.Size(16, 16)
        Me.lblBlueCount.TabIndex = 18
        Me.lblBlueCount.Text = "0"
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label3.Location = New System.Drawing.Point(54, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(451, 217)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = resources.GetString("Label3.Text")
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 508)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblBlueCount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblRedCount)
        Me.Controls.Add(Me.lblBlueTurn)
        Me.Controls.Add(Me.lblRedTurn)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblWhiteCount)
        Me.Controls.Add(Me.lblWhiteTurn)
        Me.Controls.Add(Me.lblBlackCount)
        Me.Controls.Add(Me.lblBlackTurn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblBlackTurn As Label
    Friend WithEvents lblBlackCount As Label
    Friend WithEvents lblWhiteTurn As Label
    Friend WithEvents lblWhiteCount As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents lblRedTurn As Label
    Friend WithEvents lblBlueTurn As Label
    Friend WithEvents lblRedCount As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblBlueCount As Label
    Friend WithEvents Label3 As Label
End Class
