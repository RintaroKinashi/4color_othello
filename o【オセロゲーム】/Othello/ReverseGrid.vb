Public Class ReverseGrid
    '▼Constを用いた変数の定義（フィールド）
    Public Const CellSize As Integer = 48 'セルのサイズ
    Public Const XCount As Integer = 8 '盤の横方向のセル数
    Public Const YCount As Integer = 8 '盤の縦方向のセル数
    '▼石をひっくり返した時のイベント：Reversed

    Public Event Reversed(ByVal sender As Object, ByVal e As EventArgs)
    '▼石を置いたことを示すイベント：PutNew
    Public Event PutNew(ByVal sender As Object, ByVal e As EventArgs)
    
    Dim m_Cells(XCount - 1, YCount - 1) As Cell '全セルを表す配列

    '■コンストラクタ
    ''' <summary>全セルを初期化します。</summary>
    Public Sub New()

        Dim X As Integer
        Dim Y As Integer

        For X = 0 To XCount - 1
            For Y = 0 To YCount - 1
                m_Cells(X, Y) = New Cell(Me, New Point(X, Y))
            Next
        Next

    End Sub

    '■Cells
    ''' <summary>セルを取得します。</summary>
    ''' <param name="X">セルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">セルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    ''' <returns>対象のセルを返します。</returns>
    Public Property Cells(ByVal X As Integer, ByVal Y As Integer) As Cell
        Get
            Return m_Cells(X, Y)
        End Get
        Set(ByVal value As Cell)
            m_Cells(X, Y) = value
        End Set
    End Property


    '■Draw
    ''' <summary>現在の状態を描画します。</summary>
    ''' <param name="g">描画対象のGraphicsオブジェクトを指定します。</param>
    Public Sub Draw(ByVal g As Graphics)
        'Drawメソッドの定義→メソッドはオブジェクトの動き！

        Dim X As Integer
        Dim Y As Integer
        Dim BorderPen As New Pen(Color.Black, 2)

        '▼グリッドの描画

        '深緑の四角形
        g.FillRectangle(Brushes.DarkGreen, 0, 0, XCount * CellSize, YCount * CellSize)

        '縦の9本の線
        For X = 0 To XCount
            g.DrawLine(BorderPen, X * CellSize, 0, X * CellSize, YCount * CellSize)
        Next

        '横の9本の線
        For Y = 0 To YCount
            g.DrawLine(BorderPen, 0, Y * CellSize, XCount * CellSize, Y * CellSize)
        Next
        '▼セルの状態を描画
        For Y = 0 To YCount - 1
            For X = -0 To XCount - 1
                Cells(X, Y).Draw(g)
                '◆Cellsプロパティ.Drawメソッド
                '◆自分で自分を呼び出し？動きがわからない
            Next
        Next
    End Sub


    '■Initialize
    ''' <summary>ゲームを最初の状態にします。</summary>
    Public Sub Initialize()

        'すべてのセルの状態を初期状態にする。
        For Each Cell As Cell In m_Cells
            Cell.Status = CellStatus.Nothing
        Next

        ''初期配置
        Cells(2, 2).Status = CellStatus.Red
        Cells(2, 3).Status = CellStatus.White
        Cells(2, 4).Status = CellStatus.Black
        Cells(2, 5).Status = CellStatus.Blue

        Cells(3, 2).Status = CellStatus.Blue
        Cells(3, 3).Status = CellStatus.Black
        Cells(3, 4).Status = CellStatus.White
        Cells(3, 5).Status = CellStatus.Red

        Cells(4, 2).Status = CellStatus.Black
        Cells(4, 3).Status = CellStatus.Blue
        Cells(4, 4).Status = CellStatus.Red
        Cells(4, 5).Status = CellStatus.White

        Cells(5, 2).Status = CellStatus.White
        Cells(5, 3).Status = CellStatus.Red
        Cells(5, 4).Status = CellStatus.Blue
        Cells(5, 5).Status = CellStatus.Black

        ''テスト用
        'Cells(2, 2).Status = CellStatus.Black
        'Cells(2, 3).Status = CellStatus.White
        'Cells(2, 4).Status = CellStatus.Black
        'Cells(2, 5).Status = CellStatus.Blue

        'Cells(3, 2).Status = CellStatus.Blue
        'Cells(3, 3).Status = CellStatus.Black
        'Cells(3, 4).Status = CellStatus.White
        'Cells(3, 5).Status = CellStatus.Black

        'Cells(4, 2).Status = CellStatus.Black
        'Cells(4, 3).Status = CellStatus.Blue
        'Cells(4, 4).Status = CellStatus.Black
        'Cells(4, 5).Status = CellStatus.White

        'Cells(5, 2).Status = CellStatus.White
        'Cells(5, 3).Status = CellStatus.White
        'Cells(5, 4).Status = CellStatus.Blue
        'Cells(5, 5).Status = CellStatus.Black

        ''初期配置(テスト用)
        'For x = 2 To 5
        '    For y = 2 To 5
        '        Cells(x, y).Status = CellStatus.Black
        '    Next
        'Next

    End Sub

    '■CellFromPoint
    ''' <summary>指定した座標にあるセルを取得します。</summary>
    ''' <param name="X">X座標を指定します。</param>
    ''' <param name="Y">Y座標を指定します。</param>
    ''' <returns>座標(X, Y)にあるセルを返します。該当するセルがない場合にはNothingを返します。</returns>
    Public Function CellFromPoint(ByVal X As Integer, ByVal Y As Integer) As Cell

        Dim ThisCell As Cell

        If X < 0 OrElse X >= XCount * CellSize Then
            Return Nothing
        End If

        If Y < 0 OrElse Y >= YCount * CellSize Then
            Return Nothing
        End If

        ThisCell = Cells(X \ CellSize, Y \ CellSize)

        Return ThisCell

    End Function

    '■ReversibleCount
    ''' <summary>石をおいた場合にひっくり返せる石の数を調べます。</summary>
    ''' <param name="Status">置こうとしている石の状態を指定します。</param>
    ''' <param name="X">対象のセルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">対象のセルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    ''' <returns>石をおいた場合にひっくり返せる石の数を返します。</returns>
    Public Overloads Function ReversibleCount(ByVal Status As CellStatus, ByVal X As Integer, ByVal Y As Integer) As Integer

        Dim Count As Integer

        Count = ReversibleCount(Status, ScanDirection.Left, X, Y)
        Count += ReversibleCount(Status, ScanDirection.Right, X, Y)
        Count += ReversibleCount(Status, ScanDirection.Up, X, Y)
        Count += ReversibleCount(Status, ScanDirection.Down, X, Y)
        Count += ReversibleCount(Status, ScanDirection.RightUp, X, Y)
        Count += ReversibleCount(Status, ScanDirection.RightDown, X, Y)
        Count += ReversibleCount(Status, ScanDirection.LeftUp, X, Y)
        Count += ReversibleCount(Status, ScanDirection.LeftDown, X, Y)

        Return Count

    End Function
    '■ReversibleCount
    ''' <summary>石をおいた場合に特定の方向にひっくり返せる石の数を調べます。</summary>
    ''' <param name="Status">置こうとしている石の状態を指定します。</param>
    ''' <param name="Direction">ひっくり返す方向を指定します。</param>
    ''' <param name="X">対象のセルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">対象のセルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    ''' <returns>石をおいた場合にひっくり返せる石の数を返します。</returns>
    Private Overloads Function ReversibleCount(ByVal Status As CellStatus, ByVal Direction As ScanDirection, ByVal X As Integer, ByVal Y As Integer) As Integer

        Dim AnotherStatus1 As CellStatus
        Dim AnotherStatus2 As CellStatus
        Dim AnotherStatus3 As CellStatus
        Dim XDirection As Integer '左のとき-1, 右のとき1
        Dim YDirection As Integer '上のとき-1, 下のとき1

        '相手の色を取得
        If Status = CellStatus.Black Then
            AnotherStatus1 = CellStatus.White
            AnotherStatus2 = CellStatus.Red
            AnotherStatus3 = CellStatus.Blue
        ElseIf Status = CellStatus.White Then
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.Red
            AnotherStatus3 = CellStatus.Blue
        ElseIf Status = CellStatus.Red Then
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.White
            AnotherStatus3 = CellStatus.Blue
        Else
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.White
            AnotherStatus3 = CellStatus.Red
        End If

        Select Case Direction
            Case ScanDirection.Left
                XDirection = -1
                YDirection = 0
            Case ScanDirection.Right
                XDirection = 1
                YDirection = 0
            Case ScanDirection.Up
                XDirection = 0
                YDirection = -1
            Case ScanDirection.Down
                XDirection = 0
                YDirection = 1
            Case ScanDirection.RightUp
                XDirection = 1
                YDirection = -1
            Case ScanDirection.RightDown
                XDirection = 1
                YDirection = 1
            Case ScanDirection.LeftUp
                XDirection = -1
                YDirection = -1
            Case ScanDirection.LeftDown
                XDirection = -1
                YDirection = 1
        End Select

        Dim TestX As Integer
        Dim TestY As Integer
        Dim Count As Integer
        Dim ThisCell As Cell

        '一番端に置こうとしているときはその方向にひっくり返せるわけがない
        If X + XDirection < 0 OrElse X + XDirection >= XCount OrElse Y + YDirection < 0 OrElse Y + YDirection >= YCount Then
            Return False
        End If

        '隣のセルを取得
        ThisCell = Cells(X + XDirection, Y + YDirection)

        '隣のセルの色が相手の色ならばひっくり返せる可能性がある
        If Cells(X + XDirection, Y + YDirection).Status = AnotherStatus1 OrElse Cells(X + XDirection, Y + YDirection).Status = AnotherStatus2 OrElse Cells(X + XDirection, Y + YDirection).Status = AnotherStatus3 Then

            For i As Integer = 0 To XCount - 1

                'もう１個隣の座標
                TestX = X + (XDirection * (i + 1))
                TestY = Y + (YDirection * (i + 1))

                'もう１個隣がグリッドからはみ出るなら
                '結局１個もひっくり返せないと言うこと。
                If TestX < 0 OrElse TestX > XCount - 1 Then
                    Return 0
                End If

                If TestY < 0 OrElse TestY > YCount - 1 Then
                    Return 0
                End If

                'もう１個隣の色が
                Select Case Cells(TestX, TestY).Status
                    Case Status
                        '自分と同じ色ならばひっくり返せる
                        Return Count
                    Case CellStatus.Nothing
                        '何もなければひっくり返せない
                        Return 0
                    Case Else
                        '相手の色ならばひっくり返せる可能性がある数が1増える
                        Count += 1
                End Select

            Next

        End If

        Return 0

    End Function

    '■CanPut
    ''' <summary>セルに石を置くことができるか調べます。</summary>
    ''' <param name="Status">置こうとしている石の状態を指定します。</param>
    ''' <param name="X">対象のセルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">対象のセルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    ''' <returns>セルに石が置ける場合はTrueを返します。</returns>
    Public Function CanPut(ByVal Status As CellStatus, ByVal X As Integer, ByVal Y As Integer) As Boolean

        '既に目的のセルに石が置かれているかチェック
        If Cells(X, Y).Status <> CellStatus.Nothing Then
            Return False
        End If

        'このセルに石を置いた場合ひっくり返せる石があるかチェック
        If ReversibleCount(Status, X, Y) = 0 Then
            Return False
        End If

        Return True

    End Function
    '■Reverse
    ''' <summary>石をひっくり返します。</summary>
    ''' <param name="Status">ひっくり返す原因となった石の色を指定します。</param>
    ''' <param name="Direction">ひっくり返す方向を指定します。</param>
    ''' <param name="X">対象のセルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">対象のセルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    Public Sub Reverse(ByVal Status As CellStatus, ByVal Direction As ScanDirection, ByVal X As Integer, ByVal Y As Integer)

        Dim AnotherStatus1 As CellStatus
        Dim AnotherStatus2 As CellStatus
        Dim AnotherStatus3 As CellStatus
        Dim i As Integer
        Dim XDirection As Integer '左のとき-1, 右のとき1
        Dim YDirection As Integer '上のとき-1, 下のとき1

        '相手の色を取得
        If Status = CellStatus.Black Then
            AnotherStatus1 = CellStatus.White
            AnotherStatus2 = CellStatus.Red
            AnotherStatus3 = CellStatus.Blue
        ElseIf Status = CellStatus.White Then
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.Red
            AnotherStatus3 = CellStatus.Blue
        ElseIf Status = CellStatus.Red Then
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.White
            AnotherStatus3 = CellStatus.Blue
        Else
            AnotherStatus1 = CellStatus.Black
            AnotherStatus2 = CellStatus.White
            AnotherStatus3 = CellStatus.Red
        End If

        Select Case Direction
            Case ScanDirection.Left
                XDirection = -1
                YDirection = 0
            Case ScanDirection.Right
                XDirection = 1
                YDirection = 0
            Case ScanDirection.Up
                XDirection = 0
                YDirection = -1
            Case ScanDirection.Down
                XDirection = 0
                YDirection = 1
            Case ScanDirection.RightUp
                XDirection = 1
                YDirection = -1
            Case ScanDirection.RightDown
                XDirection = 1
                YDirection = 1
            Case ScanDirection.LeftUp
                XDirection = -1
                YDirection = -1
            Case ScanDirection.LeftDown
                XDirection = -1
                YDirection = 1
        End Select

        Dim TestX As Integer
        Dim TestY As Integer

        '▼ひっくりかえす
        If ReversibleCount(Status, Direction, X, Y) > 0 Then

            For i = 1 To XCount - 1

                TestX = X + (XDirection * i)
                TestY = Y + (YDirection * i)

                If TestX < 0 OrElse TestX >= XCount Then
                    Exit For
                End If

                If TestY < 0 OrElse TestY >= YCount Then
                    Exit For
                End If

                If Cells(TestX, TestY).Status = AnotherStatus1 OrElse Cells(TestX, TestY).Status = AnotherStatus2 OrElse Cells(TestX, TestY).Status = AnotherStatus3 Then
                    Cells(TestX, TestY).Status = Status
                    RaiseEvent Reversed(Me, New EventArgs)
                Else
                    Exit For
                End If

            Next

        End If

    End Sub


    '■Put
    ''' <summary>セルに石を置きます。</summary>
    ''' <param name="Status">置く石の状態を指定します。</param>
    ''' <param name="X">対象のセルの0から始まるX位置を指定します。Xの最大値はXCount-1です。</param>
    ''' <param name="Y">対象のセルの0から始まるY位置を指定します。Yの最大値はYCount-1です。</param>
    ''' <returns>石を置いた場合Trueを返します。置けなかった場合Falseを返します。</returns>
    ''' <remarks>このメソッドを呼び出すと周辺の石がひっくり返ります。</remarks>
    Public Function Put(ByVal Status As CellStatus, ByVal X As Integer, ByVal Y As Integer) As Boolean

        '▼この位置に石が置けるか確認する
        If CanPut(Status, X, Y) = False Then
            Return False
        End If

        '▼石を置く
        Cells(X, Y).Focus()
        Cells(X, Y).Status = Status

        RaiseEvent PutNew(Me, New EventArgs)

        '▼周辺８方向の石をひっくり返す
        Call Reverse(Status, ScanDirection.Left, X, Y)
        Call Reverse(Status, ScanDirection.Right, X, Y)
        Call Reverse(Status, ScanDirection.Up, X, Y)
        Call Reverse(Status, ScanDirection.Down, X, Y)
        Call Reverse(Status, ScanDirection.LeftUp, X, Y)
        Call Reverse(Status, ScanDirection.LeftDown, X, Y)
        Call Reverse(Status, ScanDirection.RightUp, X, Y)
        Call Reverse(Status, ScanDirection.RightDown, X, Y)

        Return True

    End Function

    '■Count
    ''' <summary>状態を指定してセルの数を取得します。</summary>
    ''' <param name="Status">数えるセルの状態を指定します。</param>
    ''' <returns>Statusで指定した状態であるセルの数を返します。</returns>
    Public ReadOnly Property Count(ByVal Status As CellStatus) As Integer
        Get

            Dim ThisCount As Integer

            For Each Cell As Cell In m_Cells
                If Cell.Status = Status Then
                    ThisCount += 1
                End If
            Next

            Return ThisCount

        End Get
    End Property


    '■PuttableCount
    ''' <summary>置くことができる場所の数を調べます。</summary>
    ''' <param name="Status">調べる石の色を指定します。</param>
    ''' <returns>Statusで指定された色を置くことができる場所の数を返します。</returns>
    Public Function PuttableCount(ByVal Status As CellStatus) As Integer

        Dim Count As Integer

        For Each Cell As Cell In m_Cells
            If CanPut(Status, Cell.Position.X, Cell.Position.Y) Then
                Count += 1
            End If
        Next

        Return Count

    End Function
End Class

