Public Class Computer1
    Dim Grid As ReverseGrid
    Public Standard As CellStatus

    Public Sub New(ByVal Grid As ReverseGrid, ByVal Standard As CellStatus)

        Me.Grid = Grid
        Me.Standard = Standard

    End Sub

    Public Sub Put()

        Dim X As Integer
        Dim Y As Integer
        Dim r As New Random

        '最優先で角を取る
        If Grid.CanPut(Standard, 0, 0) Then
            Grid.Put(Standard, 0, 0)
            Return
        ElseIf Grid.CanPut(Standard, 0, ReverseGrid.YCount - 1) Then
            Grid.Put(Standard, 0, ReverseGrid.YCount - 1)
            Return
        ElseIf Grid.CanPut(Standard, ReverseGrid.XCount - 1, 0) Then
            Grid.Put(Standard, ReverseGrid.XCount - 1, 0)
            Return
        ElseIf Grid.CanPut(Standard, ReverseGrid.XCount - 1, ReverseGrid.YCount - 1) Then
            Grid.Put(Standard, ReverseGrid.XCount - 1, ReverseGrid.YCount - 1)
            Return
        Else

            '角が取れないなら、ランダムに分岐
            Dim rnd As New System.Random(1000)
            Dim i1 As Integer = rnd.Next(10)

            If i1 > 5 Then
                '一番多く取れる場所を選ぶパターン
                Dim i As Integer
                Dim dt As New DataTable("MaxPut")
                dt.Columns.Add("standard")
                dt.Columns.Add("X")
                dt.Columns.Add("Y")
                dt.Columns.Add("count")

                Dim dtRow As DataRow

                Dim count As Integer

                For X = 0 To ReverseGrid.XCount - 1
                    For Y = 0 To ReverseGrid.YCount - 1
                        If Grid.CanPut(Standard, X, Y) Then
                            count = Grid.ReversibleCount(Standard, X, Y)
                            dtRow = dt.NewRow
                            dtRow("X") = X
                            dtRow("Y") = Y
                            dtRow("count") = count

                            dt.Rows.Add(dtRow)
                        End If
                    Next
                Next

                Dim maxDatarow() As DataRow = dt.Select("count = MAX(count)", "")
                Dim maxX As Integer = maxDatarow(0).Item("X")
                Dim maxY As Integer = maxDatarow(0).Item("Y")

                Grid.Put(Standard, maxX, maxY)
                Return
            Else
                '端を優先しつつ、ランダムなパターン
                X = 0
                For Y = 0 To ReverseGrid.YCount - 1
                    If Grid.CanPut(Standard, X, Y) Then
                        Grid.Put(Standard, X, Y)
                        Return
                        Exit Sub
                    End If
                Next
                X = ReverseGrid.XCount - 1
                For Y = 0 To ReverseGrid.YCount - 1
                    If Grid.CanPut(Standard, X, Y) Then
                        Grid.Put(Standard, X, Y)
                        Return
                        Exit Sub
                    End If
                Next

                Dim cou As Integer = 0
                Do
                    X = r.Next(0, ReverseGrid.XCount - 1)
                    Y = r.Next(0, ReverseGrid.YCount - 1)
                    If Grid.CanPut(Standard, X, Y) Then
                        Grid.Put(Standard, X, Y)
                        Return
                        Exit Do
                    ElseIf cou > 20 Then

                        For X = 0 To ReverseGrid.XCount - 1
                            For Y = 0 To ReverseGrid.YCount - 1
                                If Grid.CanPut(Standard, X, Y) Then
                                    Grid.Put(Standard, X, Y)
                                    Return
                                    Exit Do
                                End If
                            Next
                        Next
                    End If
                    cou += 1
                Loop

            End If
        End If

    End Sub
End Class
