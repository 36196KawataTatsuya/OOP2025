using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace Chess {
    public partial class MainWindow : Window {
        // チェス盤のマス目を表すボタンの2次元配列
        private Button[,] squares = new Button[8, 8];

        // 駒の配置を管理する2次元配列
        private string[,] board = new string[8, 8];

        // 選択された駒の位置を格納する変数
        private (int row, int col)? selectedPiecePosition = null;

        // ゲームの状態（今回は仮で、どちらのターンかを示す）
        private bool isWhiteTurn = true;

        // ゲーム終了フラグ
        private bool gameEnded = false;

        public MainWindow() {
            InitializeComponent();
            CreateBoardUI();
            InitializePieces();
        }

        private void CreateBoardUI() {
            // 8x8のチェス盤をボタンで作成
            for (int row = 0; row < 8; row++) {
                for (int col = 0; col < 8; col++) {
                    var squareButton = new Button();
                    squareButton.Tag = new { Row = row, Col = col };
                    squareButton.Click += SquareButton_Click;
                    squareButton.FontSize = 24;

                    // 市松模様の背景色を設定
                    if ((row + col) % 2 == 0) {
                        squareButton.Background = new SolidColorBrush(Color.FromRgb(240, 217, 181)); // 薄い色
                    } else {
                        squareButton.Background = new SolidColorBrush(Color.FromRgb(181, 136, 99));  // 濃い色
                    }

                    // グリッドにボタンを追加し、配列に格納
                    Grid.SetRow(squareButton, row);
                    Grid.SetColumn(squareButton, col);
                    ChessBoardGrid.Children.Add(squareButton);
                    squares[row, col] = squareButton;
                }
            }
        }

        private void InitializePieces() {
            // 盤面を初期化
            for (int row = 0; row < 8; row++) {
                for (int col = 0; col < 8; col++) {
                    board[row, col] = null;
                }
            }

            // ポーンの配置
            for (int i = 0; i < 8; i++) {
                board[1, i] = "bp"; // 黒のポーン
                board[6, i] = "wp"; // 白のポーン
                squares[1, i].Content = "♟";
                squares[6, i].Content = "♙";
            }

            // ルーク
            board[0, 0] = "br"; board[0, 7] = "br";
            board[7, 0] = "wr"; board[7, 7] = "wr";
            squares[0, 0].Content = "♜"; squares[0, 7].Content = "♜";
            squares[7, 0].Content = "♖"; squares[7, 7].Content = "♖";

            // ナイト
            board[0, 1] = "bn"; board[0, 6] = "bn";
            board[7, 1] = "wn"; board[7, 6] = "wn";
            squares[0, 1].Content = "♞"; squares[0, 6].Content = "♞";
            squares[7, 1].Content = "♘"; squares[7, 6].Content = "♘";

            // ビショップ
            board[0, 2] = "bb"; board[0, 5] = "bb";
            board[7, 2] = "wb"; board[7, 5] = "wb";
            squares[0, 2].Content = "♝"; squares[0, 5].Content = "♝";
            squares[7, 2].Content = "♗"; squares[7, 5].Content = "♗";

            // クイーンとキング
            board[0, 3] = "bq"; board[0, 4] = "bk";
            board[7, 3] = "wq"; board[7, 4] = "wk";
            squares[0, 3].Content = "♛"; squares[0, 4].Content = "♚";
            squares[7, 3].Content = "♕"; squares[7, 4].Content = "♔";
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e) {
            if (gameEnded) return;

            var button = sender as Button;
            var position = (dynamic)button.Tag;
            int row = position.Row;
            int col = position.Col;

            if (selectedPiecePosition == null) {
                // 駒がまだ選択されていない場合
                if (board[row, col] != null) {
                    // 現在のターンの駒かチェック
                    bool isPieceWhite = board[row, col][0] == 'w';
                    if (isPieceWhite == isWhiteTurn) {
                        // 駒がクリックされた場合、その駒を選択状態にする
                        selectedPiecePosition = (row, col);
                        button.BorderBrush = Brushes.Red;
                        button.BorderThickness = new Thickness(3);
                    }
                }
            } else {
                // 駒がすでに選択されている場合
                (int startRow, int startCol) = selectedPiecePosition.Value;

                // 選択を解除するための処理
                squares[startRow, startCol].BorderBrush = null;
                squares[startRow, startCol].BorderThickness = new Thickness(1);

                // 同じマスをクリックした場合は選択解除のみ
                if (startRow == row && startCol == col) {
                    selectedPiecePosition = null;
                    return;
                }

                // 駒の移動ロジック
                bool isValidMove = IsValidMove(startRow, startCol, row, col);

                if (isValidMove) {
                    // キングを取得したかチェック
                    if (board[row, col] != null && board[row, col][1] == 'k') {
                        string winner = isWhiteTurn ? "白" : "黒";
                        MessageBox.Show($"ゲーム終了！{winner}の勝利です！", "チェス", MessageBoxButton.OK, MessageBoxImage.Information);
                        gameEnded = true;
                    }

                    // 有効な移動の場合
                    board[row, col] = board[startRow, startCol];
                    board[startRow, startCol] = null;
                    squares[row, col].Content = squares[startRow, startCol].Content;
                    squares[startRow, startCol].Content = null;

                    // ターンを切り替え
                    isWhiteTurn = !isWhiteTurn;
                    Title = $"WPF Chess - {(isWhiteTurn ? "白" : "黒")}のターン";
                }

                selectedPiecePosition = null;
            }
        }

        private bool IsValidMove(int startRow, int startCol, int endRow, int endCol) {
            // 範囲外チェック
            if (endRow < 0 || endRow >= 8 || endCol < 0 || endCol >= 8) return false;

            // 移動先に自分の駒がある場合は無効
            if (board[endRow, endCol] != null) {
                bool isStartPieceWhite = board[startRow, startCol][0] == 'w';
                bool isEndPieceWhite = board[endRow, endCol][0] == 'w';
                if (isStartPieceWhite == isEndPieceWhite) return false;
            }

            string piece = board[startRow, startCol];
            if (piece == null) return false;

            char pieceType = piece[1];

            switch (pieceType) {
                case 'p': return IsValidPawnMove(startRow, startCol, endRow, endCol, piece[0] == 'w');
                case 'r': return IsValidRookMove(startRow, startCol, endRow, endCol);
                case 'n': return IsValidKnightMove(startRow, startCol, endRow, endCol);
                case 'b': return IsValidBishopMove(startRow, startCol, endRow, endCol);
                case 'q': return IsValidQueenMove(startRow, startCol, endRow, endCol);
                case 'k': return IsValidKingMove(startRow, startCol, endRow, endCol);
            }

            return false;
        }

        private bool IsValidPawnMove(int startRow, int startCol, int endRow, int endCol, bool isWhite) {
            int direction = isWhite ? -1 : 1; // 白は上に、黒は下に移動
            int startingRow = isWhite ? 6 : 1;

            // 前進の場合
            if (endCol == startCol) {
                // 1マス前進
                if (endRow == startRow + direction && board[endRow, endCol] == null) {
                    return true;
                }
                // 初期位置から2マス前進
                if (startRow == startingRow && endRow == startRow + 2 * direction && board[endRow, endCol] == null) {
                    return true;
                }
            }
            // 斜め取り
            else if (Math.Abs(endCol - startCol) == 1 && endRow == startRow + direction) {
                return board[endRow, endCol] != null;
            }

            return false;
        }

        private bool IsValidRookMove(int startRow, int startCol, int endRow, int endCol) {
            // 縦または横の移動のみ
            if (startRow != endRow && startCol != endCol) return false;

            return IsPathClear(startRow, startCol, endRow, endCol);
        }

        private bool IsValidKnightMove(int startRow, int startCol, int endRow, int endCol) {
            int rowDiff = Math.Abs(endRow - startRow);
            int colDiff = Math.Abs(endCol - startCol);

            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        private bool IsValidBishopMove(int startRow, int startCol, int endRow, int endCol) {
            // 斜めの移動のみ
            if (Math.Abs(endRow - startRow) != Math.Abs(endCol - startCol)) return false;

            return IsPathClear(startRow, startCol, endRow, endCol);
        }

        private bool IsValidQueenMove(int startRow, int startCol, int endRow, int endCol) {
            return IsValidRookMove(startRow, startCol, endRow, endCol) ||
                   IsValidBishopMove(startRow, startCol, endRow, endCol);
        }

        private bool IsValidKingMove(int startRow, int startCol, int endRow, int endCol) {
            int rowDiff = Math.Abs(endRow - startRow);
            int colDiff = Math.Abs(endCol - startCol);
            return rowDiff <= 1 && colDiff <= 1;
        }

        private bool IsPathClear(int startRow, int startCol, int endRow, int endCol) {
            int rowDirection = endRow > startRow ? 1 : endRow < startRow ? -1 : 0;
            int colDirection = endCol > startCol ? 1 : endCol < startCol ? -1 : 0;

            int currentRow = startRow + rowDirection;
            int currentCol = startCol + colDirection;

            while (currentRow != endRow || currentCol != endCol) {
                if (board[currentRow, currentCol] != null) return false;
                currentRow += rowDirection;
                currentCol += colDirection;
            }

            return true;
        }
    }
}