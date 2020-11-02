''改善点
''　①１～４人までプレイヤー数を選択できる機能（４-プレイヤー数＝PC数）
''　 →にしようと思ったけどこのままでよくね？
''　③コードの整理


Public Class Form1

#Region "フィールド"

    Dim _Gekiha As Integer
    Private Flag_PC1 As Boolean
    Private Flag_PC2 As Boolean
    Private Flag_PC3 As Boolean

    '▼ReverseGrid型のオブジェクトGridを宣言
    Dim WithEvents Grid As New ReverseGrid

    ''' <summary>
    ''' CPの初期化
    ''' </summary>
    Dim Computer1 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Black, CellStatus.White, CellStatus.Black))
    Dim Computer2 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Red OrElse PlayerColor = CellStatus.Blue, CellStatus.White, CellStatus.Red))
    Dim Computer3 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Blue, CellStatus.Red, CellStatus.Blue))

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 残機を０にした敵の数×５点を保持しておく関数
    ''' </summary>
    ''' <returns></returns>
    Private Property Gekiha As Integer
        Get
            Return _Gekiha
        End Get
        Set(value As Integer)
            _Gekiha = value
        End Set
    End Property

    Public Property Flag_PC11 As Boolean
        Get
            Return Flag_PC1
        End Get
        Set(value As Boolean)
            Flag_PC1 = value
        End Set
    End Property

    Public Property Flag_PC21 As Boolean
        Get
            Return Flag_PC2
        End Get
        Set(value As Boolean)
            Flag_PC2 = value
        End Set
    End Property

    Public Property Flag_PC31 As Boolean
        Get
            Return Flag_PC3
        End Get
        Set(value As Boolean)
            Flag_PC3 = value
        End Set
    End Property

#End Region

#Region "ロード時の処理"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'オセロの2×2の初期状態を表示
        Grid.Initialize()
        lblBlackCount.Text = Grid.Count(CellStatus.Black)
        lblWhiteCount.Text = Grid.Count(CellStatus.White)
        lblRedCount.Text = Grid.Count(CellStatus.Red)
        lblBlueCount.Text = Grid.Count(CellStatus.Blue)
        lblBlackTurn.Visible = False
        lblWhiteTurn.Visible = False
        lblRedTurn.Visible = False
        lblBlueTurn.Visible = False

        PictureBox1.Visible = False
    End Sub
#End Region

#Region "描画処理"
    ''' <summary>
    ''' 描画処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        '▼オブジェクトGridからDrawメソッドを呼び出す
        Grid.Draw(e.Graphics)
    End Sub

    ''' <summary>
    ''' 盤面をクリックしたときの処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        'マウスの座標をPictureBox1のコントロール座標に変換する。
        Dim Pos As Point = PictureBox1.PointToClient(Windows.Forms.Cursor.Position)
        Dim ThisCell As Cell

        ThisCell = Grid.CellFromPoint(Pos.X, Pos.Y)

        If Grid.Put(Turn, ThisCell.Position.X, ThisCell.Position.Y) Then
            ChangeTurn()
        End If
    End Sub

    ''' <summary>マウスの移動に伴ってセルにアクティブを示す枠を描画する</summary>
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove

        Dim ThisCell As Cell

        'マウスがある位置のセルを取得
        ThisCell = Grid.CellFromPoint(e.X, e.Y)

        If Not IsNothing(ThisCell) Then

            'セルが取得できた場合は、セルにアクティブを示す枠を描画
            ThisCell.Focus()

            '現在の状態を描画(PictureBox1のPaintイベントを発生させる)
            PictureBox1.Invalidate() '←実際の描画はすべてここで行う
        End If
    End Sub

    ''' <summary>石を置いたときに発生するイベント</summary>
    Private Sub Grid_PutNew(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PutNew
        Call Grid_Reversed(sender, e)
    End Sub

    ''' <summary>石がひっくり返されたときに発生するイベント</summary>
    Private Sub Grid_Reversed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.Reversed

        '現在の状態を描画(PictureBox1のPaintイベントを発生させる)
        PictureBox1.Invalidate()

        lblBlackCount.Text = Grid.Count(CellStatus.Black)
        lblWhiteCount.Text = Grid.Count(CellStatus.White)
        lblRedCount.Text = Grid.Count(CellStatus.Red)
        lblBlueCount.Text = Grid.Count(CellStatus.Blue)


        'ちょっと時間をおく
        Application.DoEvents()
        System.Threading.Thread.Sleep(300)

    End Sub
#End Region

#Region "ターン・勝敗決定処理"
    Dim Turn As CellStatus = CellStatus.Black '今どっちの順番か
    Dim PlayerColor As CellStatus = CellStatus.Black 'プレイヤーの色

    '■ChangeTurn
    ''' <summary>ターン交代</summary>
    Public Sub ChangeTurn()

        '現在の状態を描画(PictureBox1のPaintイベントを発生させる)
        PictureBox1.Invalidate()

        Dim Winner As CellStatus = CellStatus.Nothing

        '▼勝敗判定
        If Grid.PuttableCount(CellStatus.Black) = 0 AndAlso Grid.PuttableCount(CellStatus.White) = 0 AndAlso Grid.PuttableCount(CellStatus.Blue) = 0 AndAlso Grid.PuttableCount(CellStatus.Red) = 0 Then

            '試合終了　＝　全プレイヤーが置けない状況になる
            If Grid.Count(PlayerColor) > Grid.Count(Computer1.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer2.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの勝ちです！")
            ElseIf Grid.Count(PlayerColor) < Grid.Count(Computer1.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer2.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの負けです！")
            Else
                MsgBox("引き分けです！！")
            End If
            Ranking(PlayerColor, Winner_Decision(PlayerColor, Computer1.Standard, Computer2.Standard, Computer3.Standard))
            Exit Sub
        ElseIf Grid.Count(Computer1.Standard) = 0 AndAlso Grid.Count(Computer2.Standard) = 0 AndAlso Grid.Count(Computer3.Standard) = 0 Then
            'すべての石がPlayerColorになった場合
            MsgBox("あなた勝ちです！")
            Ranking(PlayerColor, Winner_Decision(PlayerColor, Computer1.Standard, Computer2.Standard, Computer3.Standard))
            Exit Sub
        ElseIf Grid.Count(PlayerColor) = 0 Then
            'すべての石がComputerになった場合
            MsgBox("あなたの負けです！")
            Ranking(PlayerColor, Winner_Decision(PlayerColor, Computer1.Standard, Computer2.Standard, Computer3.Standard))
            Exit Sub
        End If


        ''プレイヤーターンのときに敵を撃破で５点加算！
        If Turn = PlayerColor AndAlso Flag_PC11 = False Then
            If Grid.Count(Computer1.Standard) = 0 Then
                Gekiha += 5
                Flag_PC11 = True
                MsgBox("敵を撃破！　ただいまのボーナス点：" & Gekiha & "point")
            End If
        End If

        If Turn = PlayerColor AndAlso Flag_PC21 = False Then
            If Grid.Count(Computer2.Standard) = 0 Then
                Gekiha += 5
                Flag_PC21 = True
                MsgBox("敵を撃破！　ただいまのボーナス点：" & Gekiha & "point")
            End If
        End If

        If Turn = PlayerColor AndAlso Flag_PC31 = False Then
            If Grid.Count(Computer3.Standard) = 0 Then
                Gekiha += 5
                Flag_PC31 = True
                MsgBox("敵を撃破！　ただいまのボーナス点：" & Gekiha & "point")
            End If
        End If


        '▼次のターンの決定
        If Turn = CellStatus.Black Then
            Turn = CellStatus.White
            lblBlackTurn.Visible = False
            lblWhiteTurn.Visible = True
        ElseIf Turn = CellStatus.White Then
            Turn = CellStatus.Red
            lblWhiteTurn.Visible = False
            lblRedTurn.Visible = True
        ElseIf Turn = CellStatus.Red Then
            Turn = CellStatus.Blue
            lblRedTurn.Visible = False
            lblBlueTurn.Visible = True
        Else
            Turn = CellStatus.Black
            lblBlueTurn.Visible = False
            lblBlackTurn.Visible = True
        End If

        '▼置ける場所があるか判定
        If Grid.PuttableCount(Turn) = 0 Then
            '置く場所がなければパスして次のターン
            ChangeTurn()
        End If

        '▼人間かコンピュータかで処理を分岐
        If Turn = PlayerColor Then
            '人間の番ならば、PictureBoxを使用可能にする。
            PictureBox1.Enabled = True
        Else
            'コンピュータの番ならば、PictureBoxを使用不可にする。
            PictureBox1.Enabled = False

            'ちょっと時間をおく
            Application.DoEvents()
            System.Threading.Thread.Sleep(500)

            'コンピュータに石を置かせる。どのセルに置くかはコンピュータ(AI)が決定する。

            If PlayerColor = CellStatus.Black Then
                If Turn = CellStatus.White Then
                    Computer1.Put()
                ElseIf Turn = CellStatus.Red Then
                    Computer2.Put()
                ElseIf Turn = CellStatus.Blue Then
                    Computer3.Put()
                End If
            ElseIf PlayerColor = CellStatus.White Then

                If Turn = CellStatus.Black Then
                    Computer1.Put()
                ElseIf Turn = CellStatus.Red Then
                    Computer2.Put()
                ElseIf Turn = CellStatus.Blue Then
                    Computer3.Put()
                End If
            ElseIf PlayerColor = CellStatus.Red Then

                If Turn = CellStatus.Black Then
                    Computer1.Put()
                ElseIf Turn = CellStatus.White Then
                    Computer2.Put()
                ElseIf Turn = CellStatus.Blue Then
                    Computer3.Put()
                End If
            Else
                If Turn = CellStatus.Black Then
                    Computer1.Put()
                ElseIf Turn = CellStatus.White Then
                    Computer2.Put()
                ElseIf Turn = CellStatus.Red Then
                    Computer3.Put()
                End If
            End If

            If Grid.Count(Computer1.Standard) = 0 Then
                Flag_PC11 = True
            End If
            If Grid.Count(Computer2.Standard) = 0 Then
                Flag_PC21 = True
            End If
            If Grid.Count(Computer3.Standard) = 0 Then
                Flag_PC31 = True
            End If

            ChangeTurn()
        End If

    End Sub

    ''' <summary>
    ''' 勝者を返す関数
    ''' </summary>
    ''' <param name="Player"></param>
    ''' <param name="PC1"></param>
    ''' <param name="PC2"></param>
    ''' <param name="PC3"></param>
    ''' <returns></returns>
    Private Function Winner_Decision(Player As CellStatus, PC1 As CellStatus, PC2 As CellStatus, PC3 As CellStatus) As CellStatus
        Dim Winner As CellStatus = Player

        If Grid.Count(Winner) < Grid.Count(PC1) Then
            Winner = PC1
        End If
        If Grid.Count(Winner) < Grid.Count(PC2) Then
            Winner = PC2
        End If
        If Grid.Count(Winner) < Grid.Count(PC3) Then
            Winner = PC3
        End If

        Return Winner

    End Function
#End Region

#Region "ボタン押下時"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PictureBox1.Visible = True
        Start(CellStatus.Black)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox1.Visible = True
        Start(CellStatus.White)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PictureBox1.Visible = True
        Start(CellStatus.Red)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PictureBox1.Visible = True
        Start(CellStatus.Blue)
    End Sub
#End Region

#Region "ゲームスタート時"
    '■Start
    ''' <summary>ゲームを開始します。</summary>
    ''' <param name="PlayerColor">人間の石の色を指定します。</param>
    ''' <remarks>黒が先手になります。</remarks>
    Private Sub Start(ByVal PlayerColor As CellStatus)

        PictureBox1.Visible = True
        Flag_PC11 = False
        Flag_PC21 = False
        Flag_PC31 = False

        Grid.Initialize()
        '初期化
        Me.PlayerColor = CellStatus.Nothing
        Computer1.Standard = CellStatus.Nothing
        Computer2.Standard = CellStatus.Nothing
        Computer3.Standard = CellStatus.Nothing
        Gekiha = 0

        Me.PlayerColor = PlayerColor '人間の色

        If PlayerColor = CellStatus.Black Then
            Computer1.Standard = CellStatus.White
            Computer2.Standard = CellStatus.Red
            Computer3.Standard = CellStatus.Blue
        ElseIf PlayerColor = CellStatus.White Then
            Computer1.Standard = CellStatus.Black
            Computer2.Standard = CellStatus.Red
            Computer3.Standard = CellStatus.Blue
        ElseIf PlayerColor = CellStatus.Blue Then
            Computer1.Standard = CellStatus.Black
            Computer2.Standard = CellStatus.White
            Computer3.Standard = CellStatus.Red
        Else
            Computer1.Standard = CellStatus.Black
            Computer2.Standard = CellStatus.White
            Computer3.Standard = CellStatus.Blue
        End If

        '現在の黒と白の駒の数を表示する
        lblBlackCount.Text = Grid.Count(CellStatus.Black)
        lblWhiteCount.Text = Grid.Count(CellStatus.White)
        lblRedCount.Text = Grid.Count(CellStatus.Red)
        lblBlueCount.Text = Grid.Count(CellStatus.Blue)

        'ChangeTurnを呼び出して黒の番を開始する。そのために仮に今は白の番であることにする。
        Turn = CellStatus.Blue
        ChangeTurn()

    End Sub
#End Region

#Region "ランキング処理"
    Private Sub Ranking(ByVal PlayerColor As CellStatus, ByVal Winner As CellStatus)

        Dim msg As String, namae As String, jyuni As Integer
        Dim Path As String = "Record.txt"
        Dim Path2 As String = "Record2.txt"
        Dim objFile As New System.IO.StreamReader(Path)
        Dim sr As New System.IO.StreamWriter(Path2)

        Dim point As Integer = 0

        'プレイヤーが勝利した場合
        If PlayerColor = Winner Then

            point = Grid.Count(PlayerColor)
            If Grid.Count(CellStatus.Nothing) > 0 Then
                point += Grid.Count(CellStatus.Nothing) * 2
            End If
            point += Gekiha

            If point < 20 Then
                MsgBox("スコア：" & point & "! ギリギリ勝利じゃん、まだまだだね")
            ElseIf point >= 20 And point < 30 Then
                MsgBox("スコア：" & point & "! イイ感じ、スコア30以上を目指そう！")
            ElseIf point >= 30 And point < 40 Then
                MsgBox("スコア：" & point & "! すごーい！")
            ElseIf point >= 40 And point < 50 Then
                MsgBox("スコア：" & point & "! 天才じゃん、IQ 120！")
            ElseIf point >= 50 And point < 60 Then
                MsgBox("スコア：" & point & "! IQノルマン越え、歴史に名を刻め！")
            ElseIf point >= 60 And point < 70 Then
                MsgBox("スコア：" & point & "!! 君はこの世界の王になる！神の領域まであと一歩！")
            Else
                MsgBox("スコア：" & point & "!! 君は今日から神様だ！その知能で世界を変えろ！")
            End If
        End If


        Dim strLine As String       '１行
        Dim strTemp() As String     '戻り配列
        Dim strData As String       'データ
        Dim work As String, Works As Integer    '作業用変数
        Dim work2 As String, Works2 As Integer

        Dim Rankin As Boolean = False

        '次の行へ
        strLine = objFile.ReadLine()
        For jyuni = 1 To 5

            '行単位データをカンマ部分で分割し、配列へ格納
            strTemp = Split(strLine, ",")

            'ランキングに変更があった場合に、順位を更新する処理
            If Rankin = True Then
                work2 = strTemp(0)
                Works2 = strTemp(1)

                strTemp(0) = work
                strTemp(1) = Works

                work = work2
                Works = Works2
            End If

            'ポイントが履歴と並んでも、最新の記録を優先
            If point >= CInt(strTemp(1)) And Rankin = False Then
                'ランキング内処理
                msg = ("おめでとうございます！ランキング" & jyuni & "位です！") & vbCrLf
                msg &= "名前を入力してください。"
                namae = InputBox(msg)
                Rankin = True

                work = strTemp(0)
                Works = strTemp(1)

                strTemp(0) = namae
                strTemp(1) = point
            End If


            '▼書き込み処理
            sr.Write(strTemp(0))
            sr.Write(","c)
            sr.Write(strTemp(1))
            sr.Write(vbCrLf)

            strData = strData + CStr(jyuni) + "位　" + strTemp(UBound(strTemp) - 1) + "さん　　スコア：　" + strTemp(UBound(strTemp)) + "点" & vbCrLf

            '次の行へ
            strLine = objFile.ReadLine()
        Next

        '▼ランキング表出力
        MsgBox(strData)

        'ファイルのクローズ
        objFile.Close()
        sr.Close()

        If My.Computer.FileSystem.FileExists(Path) Then
            System.IO.File.Delete(Path)
        End If

        My.Computer.FileSystem.RenameFile(Path2, "Record.txt")
        PictureBox1.Visible = False

        Application.Restart()
    End Sub
#End Region
    
End Class
