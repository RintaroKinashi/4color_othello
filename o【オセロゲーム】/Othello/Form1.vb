''改善点
''　①１～４人までプレイヤー数を選択できる機能（４-プレイヤー数＝PC数）
''　②色＝順番を決めることができる機能　→　◎
''　③コードの整理
''　④ランキング機能の追加　→　CSVのテキストファイルでOK。自分の石の数＝ポイント。Nothingセル一つにつき２ポイント加算。
''　　→プレイヤーターンに敵撃破で5ポイント加算 →　ランキング機能は穴抜けゲームを参考に


Public Class Form1

    Dim _Gekiha As Integer

    Private Property Gekiha As Integer
        Get
            Return _Gekiha
        End Get
        Set(value As Integer)
            _Gekiha = value
        End Set
    End Property

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'オセロの2×2の初期状態を表示
        Grid.Initialize()
        lblBlackCount.Text = Grid.Count(CellStatus.Black)
        lblWhiteCount.Text = Grid.Count(CellStatus.White)
        lblRedCount.Text = Grid.Count(CellStatus.Red)
        lblBlueCount.Text = Grid.Count(CellStatus.Blue)
        lblWhiteTurn.Visible = False
        lblRedTurn.Visible = False
        lblBlueTurn.Visible = False

        PictureBox1.Visible = False
    End Sub

    '▼ReverseGrid型のオブジェクトGridを宣言
    Dim WithEvents Grid As New ReverseGrid

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

    Dim Turn As CellStatus = CellStatus.Black '今どっちの順番か
    Dim PlayerColor As CellStatus = CellStatus.Black 'プレイヤーの色

    '■ChangeTurn
    ''' <summary>ターン交代</summary>
    Public Sub ChangeTurn()

        '現在の状態を描画(PictureBox1のPaintイベントを発生させる)
        PictureBox1.Invalidate()

        Dim Winner As CellStatus = CellStatus.Nothing

        '▼勝敗判定
        If Grid.Count(CellStatus.Nothing) = 0 Then

            '全セルへの配置が終了した場合は勝敗判定して終了
            If Grid.Count(PlayerColor) > Grid.Count(Computer1.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer2.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの勝ちです！")
                Ranking(PlayerColor)
            ElseIf Grid.Count(PlayerColor) < Grid.Count(Computer1.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer2.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの負けです！")
            Else
                MsgBox("引き分けです！！")
            End If

            Return
            Exit Sub
        ElseIf Grid.PuttableCount(CellStatus.Black) = 0 AndAlso Grid.PuttableCount(CellStatus.White) = 0 AndAlso Grid.PuttableCount(CellStatus.Blue) = 0 AndAlso Grid.PuttableCount(CellStatus.Red) = 0 Then
            '空いているセルがあるのに置けない場合
            If Grid.Count(PlayerColor) > Grid.Count(Computer1.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer2.Standard) AndAlso Grid.Count(PlayerColor) > Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの勝ちです！")
                Ranking(PlayerColor)
            ElseIf Grid.Count(PlayerColor) < Grid.Count(Computer1.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer2.Standard) OrElse Grid.Count(PlayerColor) < Grid.Count(Computer3.Standard) Then
                MsgBox("あなたの負けです！")
            Else
                MsgBox("引き分けです！！")
            End If

            Return
            Exit Sub
        ElseIf Grid.Count(Computer1.Standard) = 0 AndAlso Grid.Count(Computer2.Standard) = 0 AndAlso Grid.Count(Computer3.Standard) = 0 Then
            'すべての石がPlayerColorになった場合
            MsgBox("あなた勝ちです！")
            Ranking(PlayerColor)
            Return
            Exit Sub
        ElseIf Grid.Count(PlayerColor) = 0 Then
            'すべての石がComputerになった場合
            MsgBox("あなたの負けです！")
            Return
            Exit Sub
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

            ChangeTurn()
        End If

    End Sub


    ''' <summary>石を置いたときに発生するイベント</summary>
    Private Sub Grid_PutNew(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PutNew
        'Gridの宣言？？

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

        ''プレイヤーターンのときに敵を撃破で５点加算！
        If Grid.Count(CellStatus.Black) = 0 OrElse Grid.Count(CellStatus.White) = 0 OrElse Grid.Count(CellStatus.Red) = 0 OrElse Grid.Count(CellStatus.Blue) = 0 Then
            If Turn = PlayerColor Then
                Gekiha += 5
            End If
        End If

        'ちょっと時間をおく
        Application.DoEvents()
        System.Threading.Thread.Sleep(500)

    End Sub

    Dim Computer1 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Black, CellStatus.White, CellStatus.Black))
    Dim Computer2 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Red OrElse PlayerColor = CellStatus.Blue, CellStatus.White, CellStatus.Red))
    Dim Computer3 As New Computer1(Grid, IIf(PlayerColor = CellStatus.Blue, CellStatus.Red, CellStatus.Blue))

    '■Start
    ''' <summary>ゲームを開始します。</summary>
    ''' <param name="PlayerColor">人間の石の色を指定します。</param>
    ''' <remarks>黒が先手になります。</remarks>
    Private Sub Start(ByVal PlayerColor As CellStatus)

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

    Private Sub Ranking(ByVal PlayerColor As CellStatus)
        
        Dim msg As String, namae As String, jyuni As Integer
        Dim Path As String = "Record.txt"
        Dim Path2 As String = "Record2.txt"
        Dim objFile As New System.IO.StreamReader(Path)
        Dim sr As New System.IO.StreamWriter(Path2)

        Dim point As Integer = Grid.Count(PlayerColor)
        If Grid.Count(CellStatus.Nothing) > 0 Then
            point += Grid.Count(CellStatus.Nothing) * 2
        End If
        point += Gekiha

        If point < 20 Then
            MsgBox("スコア：" & point & "! ギリギリ勝利じゃん、まだまだだね")
        ElseIf point >= 20 And point < 30
            MsgBox("スコア：" & point & "! イイ感じ、スコア30以上を目指そう！")
        ElseIf point >= 30 And point < 40
            MsgBox("スコア：" & point & "! すごーい！")
        ElseIf point >= 40 And point < 50
            MsgBox("スコア：" & point & "! 天才じゃん、IQ 120！")
        ElseIf point >= 50 And point < 60
            MsgBox("スコア：" & point & "! IQノルマン越え、歴史に名を刻め！")
        ElseIf point >= 60 And point < 70
            MsgBox("スコア：" & point & "!! 君はこの世界の王になる！神の領域まであと一歩！")
        Else
            MsgBox("スコア：" & point & "!! 神じゃん！その知能で世界を作り直せ！")
        End If



        Dim strLine As String       '１行
        Dim strTemp() As String     '戻り配列
        Dim strData As String       'データ

        Dim Rankin As Boolean = False

        '次の行へ
        strLine = objFile.ReadLine()
        For jyuni = 1 To 5

            '行単位データをカンマ部分で分割し、配列へ格納
            strTemp = Split(strLine, ",")

            If point > CInt(strTemp(1)) And Rankin = False Then
                'ランキング内処理
                msg = ("おめでとうございます！ランキング" & jyuni & "位です！") & vbCrLf
                msg &= "名前を入力してください。"
                namae = InputBox(msg)
                Rankin = True

                strTemp(0) = namae
                strTemp(1) = point
            End If

            sr.Write(strTemp(0))
            sr.Write(","c)
            sr.Write(strTemp(1))
            sr.Write(vbCrLf)

            strData = strData + CStr(jyuni) + "位　" + strTemp(UBound(strTemp) - 1) + "さん　　スコア：　" + strTemp(UBound(strTemp)) + "点" & vbCrLf

            '次の行へ
            strLine = objFile.ReadLine()
        Next

        'デバッグでテキストフィールドへ書きだします。
        MsgBox(strData)

        'ファイルのクローズ
        objFile.Close()
        sr.Close()

        If My.Computer.FileSystem.FileExists(Path) Then
            System.IO.File.Delete(Path)
        End If
        ''[test] フルパスでなくても動作可能かどうか
        'If My.Computer.FileSystem.FileExists("C:\Users\kinashi\source\repos\o【オセロゲーム】\Othello\bin\Debug\" & Path) Then
        '    System.IO.File.Delete("C:\Users\kinashi\source\repos\o【オセロゲーム】\Othello\bin\Debug\" & Path)
        'End If

        My.Computer.FileSystem.RenameFile(Path2, "Record.txt")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        button_hid()
        Start(CellStatus.Black)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        button_hid()
        Start(CellStatus.White)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        button_hid()
        Start(CellStatus.Red)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        button_hid()
        Start(CellStatus.Blue)
    End Sub

    Private Sub button_hid()
        PictureBox1.Visible = True
        'Button1.Visible = False
        'Button2.Visible = False
        'Button3.Visible = False
        'Button4.Visible = False
    End Sub
End Class
