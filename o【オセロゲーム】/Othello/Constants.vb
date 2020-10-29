
'コンスタント = 常にooし続けるという意味

'列挙体定義用のクラス

'■CellStatus
''' <summary>セルの状態を表します。</summary>
Public Enum CellStatus
    [Nothing] 'なし []←初期値を表す？
    Black '黒
    White '白
    Red   '赤
    Blue  '青
End Enum


'■ScanDirection
''' <summary>方向を表します。</summary>
Public Enum ScanDirection
    Left
    Right
    Up
    Down
    LeftUp
    LeftDown
    RightUp
    RightDown
End Enum