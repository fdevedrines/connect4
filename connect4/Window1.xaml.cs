using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace connect4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        int MoveCounter = 0;
        int Row = 6;
        int Player1Score = 0;
        int Player2Score = 0;
        int GameNumber = 0;
        int NumberOfPlayers = 0;


        PlayerTurn[,] Connect4Grid = new PlayerTurn[7, 7];
        PlayerTurn Player = new PlayerTurn();

        public Window1()
        {
            InitializeComponent();
            //Disable Column buttons until the number of players is known
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            button5.IsEnabled = false;
            button6.IsEnabled = false;
            button7.IsEnabled = false;
            //Draw Ellipses to reflect Grid holes
            DrawGridHoles();
            //Add code here to prompt for 1 or 2 players

            // Centralize handling of all clicks in the Connect4Grid.
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnButtonClick));
        }

        //Define DependencyProperty
        public static DependencyProperty SelectedCol;

        static Window1()
        {
            SelectedCol = DependencyProperty.Register("InputCol", typeof(int), typeof(Window1),
                                                      new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        //Expose Dependency Property as CLR property
        public int InputCol
        {
            set { SetValue(SelectedCol, value); }
            get { return (int)GetValue(SelectedCol); }
        }

        void OnButtonClick(Object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;

            //Make sure the object exists
            if (b != null)
            {

                switch (b.Name)
                {
                    case "OnePlayer":
                        NumberOfPlayers = 1;
                        OnePlayer.IsEnabled = false;
                        TwoPlayers.IsEnabled = false;
                        button1.IsEnabled = true;
                        button2.IsEnabled = true;
                        button3.IsEnabled = true;
                        button4.IsEnabled = true;
                        button5.IsEnabled = true;
                        button6.IsEnabled = true;
                        button7.IsEnabled = true;
                        //add code so the UI shows it's a one player game
                        break;

                    case "TwoPlayers":
                        NumberOfPlayers = 2;
                        OnePlayer.IsEnabled = false;
                        TwoPlayers.IsEnabled = false;
                        button1.IsEnabled = true;
                        button2.IsEnabled = true;
                        button3.IsEnabled = true;
                        button4.IsEnabled = true;
                        button5.IsEnabled = true;
                        button6.IsEnabled = true;
                        button7.IsEnabled = true;
                        //add code so the UI shows it's a two player game
                        break;

                    case "reset":
                        //Connect4 is reset
                        InitializeComponent();
                        Connect4Grid = new PlayerTurn[7, 7];
                        Player = PlayerTurn.NotPlayed;
                        MoveCounter = 0;
                        //Clear objects in Grid
                        myGrid.Children.Clear();
                        OnePlayer.IsEnabled = true;
                        TwoPlayers.IsEnabled = true;
                        //Ensure all buttons are disabled
                        button1.IsEnabled = false;
                        button2.IsEnabled = false;
                        button3.IsEnabled = false;
                        button4.IsEnabled = false;
                        button5.IsEnabled = false;
                        button6.IsEnabled = false;
                        button7.IsEnabled = false;
                        break;

                    case "button1":
                        InputCol = 0;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;
                    case "button2":
                        InputCol = 1;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;
                    case "button3":
                        InputCol = 2;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;
                    case "button4":
                        InputCol = 3;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;

                    case "button5":
                        InputCol = 4;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;

                    case "button6":
                        InputCol = 5;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;

                    case "button7":
                        InputCol = 6;
                        Player = GetPlayerTurn(Player);
                        MakeAMove(Player, Connect4Grid, InputCol);
                        if ((NumberOfPlayers == 1) && (GameOver(CheckConnections(Connect4Grid, Row, InputCol)) == false))
                        {
                            Player = GetPlayerTurn(Player);
                            FindBestMove();
                        }
                        break;
                }


            }
        }

        //Draw Ellipses to reflect Grid holes
        void DrawGridHoles()
        {
            int i;
            int j;

            for (i = 0; i <= 5; i++)
            {
                for (j = 0; j <= 6; j++)
                {
                    Ellipse myBlankEllipse = new Ellipse();
                    myBlankEllipse.Margin = new Thickness(3);
                    myBlankEllipse.StrokeThickness = 5;
                    myBlankEllipse.Stroke = Brushes.LightBlue;
                    myBlankEllipse.Fill = Brushes.Silver;
                    Grid.SetRow(myBlankEllipse, i);
                    Grid.SetColumn(myBlankEllipse, j);
                    myGridHoles.Children.Add(myBlankEllipse);
                }
            }
        }

        void MakeAMove(PlayerTurn Player, PlayerTurn[,] Connect4Grid, int InputCol)
        {
            Row = GetRowNumber(InputCol);
            Connect4State(Connect4Grid, Row, InputCol);
            MoveCounter++;
            if (MoveCounter >= 7)
            {
                if (CheckConnections(Connect4Grid, Row, InputCol) == true)
                {
                    GameOver(CheckConnections(Connect4Grid, Row, InputCol));
                    MessageBox.Show("GAME OVER! Click Reset to play again.");
                }
            }
            Row = GetRowNumber(InputCol);
            Row = Row + 1;
        }

        //Functions
        //Identify the Row number the chip will fall in
        private int GetRowNumber(int InputCol)
        {
            int myRow = 6;

            if (Connect4Grid[myRow, InputCol] == PlayerTurn.NotPlayed)
            {
                return myRow;
            }
            else
            {
                while (Connect4Grid[myRow, InputCol] != PlayerTurn.NotPlayed)
                {
                    //Disable button when column is full
                    #region Disable buttons
                    if (myRow == 1)
                    {

                        switch (InputCol)
                        {
                            case 0:
                                button1.IsEnabled = false;
                                break;
                            case 1:
                                button2.IsEnabled = false;
                                break;
                            case 2:
                                button3.IsEnabled = false;
                                break;
                            case 3:
                                button4.IsEnabled = false;
                                break;
                            case 4:
                                button5.IsEnabled = false;
                                break;
                            case 5:
                                button6.IsEnabled = false;
                                break;
                            case 6:
                                button7.IsEnabled = false;
                                break;
                        }
                        //myRow = 0;
                        //return myRow;
                    #endregion
                    }
                    myRow--;
                }
                return myRow;
            }
        }
        //Update the Connect4 Grid
        private PlayerTurn Connect4State(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {

            Ellipse myEllipse = new Ellipse();
            myEllipse.Margin = new Thickness(4);
            myEllipse.StrokeThickness = 15;

            if (Player == PlayerTurn.Player1)
            {
                myEllipse.Stroke = Brushes.Firebrick;
                myEllipse.Fill = Brushes.DarkRed;
            }
            if (Player == PlayerTurn.Player2)
            {
                myEllipse.Stroke = Brushes.Yellow;
                myEllipse.Fill = Brushes.Gold;
            }

            Grid.SetRow(myEllipse, Row - 1);
            Grid.SetColumn(myEllipse, InputCol);
            myGrid.Children.Add(myEllipse);


            //Handle First Move
            if (MoveCounter == 0)
            {
                return Connect4Grid[Row, InputCol] = PlayerTurn.Player1;
            }
            else
            {
                return Connect4Grid[Row, InputCol] = Player;
            }

        }

        //Check if we have a Connect4 after a given move
        private Boolean CheckConnections(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckConnections = false;
            int MinRow = 1;
            int MinCol = 0;
            int MaxRow = 6;
            int MaxCol = 6;
            int myRow = Row;
            int myCol = InputCol;
            int NumberOfConnectedChips = 1;

            #region Check for horizontal Connect4
            //Check for horizontal Connect4

            //Is there a column on the right?           
            if (myCol < MaxCol)
            {
                while ((Connect4Grid[Row, InputCol] == Connect4Grid[Row, myCol]) && (myCol < MaxCol))
                {
                    if (InputCol != myCol)
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                    myCol = myCol + 1;
                }
                if ((myCol == MaxCol) && Connect4Grid[Row, myCol] == (Connect4Grid[Row, MaxCol - 1]))
                {
                    NumberOfConnectedChips = NumberOfConnectedChips + 1;
                }
            }

            //Is there a column on the left?
            myCol = InputCol;

            if (myCol > MinCol)
            {
                while ((Connect4Grid[Row, myCol] == Connect4Grid[Row, InputCol]) && (myCol > MinCol))
                {
                    if (myCol != InputCol)
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                    myCol = myCol - 1;
                }
                if ((myCol == MinCol) && Connect4Grid[Row, myCol] == (Connect4Grid[Row, MinCol + 1]))
                {
                    NumberOfConnectedChips = NumberOfConnectedChips + 1;
                }
            }

            if (NumberOfConnectedChips >= 4)
            {
                CheckConnections = true;
                return CheckConnections;
            }
            else
            {
                NumberOfConnectedChips = 1;
                CheckConnections = false;
            }
            #endregion
            #region Check for vertical Connect4
            //Check for vertical Connect4

            //Is there at least 3 rows below the chip?
            if (Row <= 4)
            {
                while ((Connect4Grid[myRow, InputCol] == Connect4Grid[Row, InputCol]) && (myRow < MaxRow))
                {
                    if (myRow != Row)
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                    myRow = myRow + 1;
                }
                if ((myRow == MaxRow) && (Connect4Grid[myRow, InputCol] == Connect4Grid[MaxRow - 1, InputCol]))
                {
                    NumberOfConnectedChips = NumberOfConnectedChips + 1;
                }
            }

            if (NumberOfConnectedChips >= 4)
            {
                CheckConnections = true;
                return CheckConnections;
            }
            else
            {
                NumberOfConnectedChips = 1;
                CheckConnections = false;
            }
            #endregion
            #region Check for diagonal Connect4
            //A diagonal Connect4 is possible only after a minimum of 10 moves
            //A potential diagona Connect4 is possible after a minimun of 9 moves

            if (MoveCounter < 9)
            {
                CheckConnections = false;
                return CheckConnections;
            }
            else
            {
                #region Check for descending diagonal Connect4
                //Check for descending diagonal Connect4
                //Is there a column on the right? Has the bottom row been reached?

                myRow = Row;
                myCol = InputCol;

                if ((myCol < MaxCol) && (myRow < MaxRow))
                {
                    while ((Connect4Grid[myRow, myCol] == Connect4Grid[Row, InputCol]) && (myCol < MaxCol) && (myRow < MaxRow))
                    {
                        if (InputCol != myCol)
                        {
                            NumberOfConnectedChips = NumberOfConnectedChips + 1;
                        }
                        myCol = myCol + 1;
                        myRow = myRow + 1;
                    }
                    if (((myCol == MaxCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] == (Connect4Grid[myRow - 1, myCol - 1]))
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                }

                //Is there a column on the left? Has the top row been reached?
                myRow = Row;
                myCol = InputCol;

                if ((myCol > MinCol) && (myRow > MinRow))
                {
                    while ((Connect4Grid[myRow, myCol] == Connect4Grid[Row, InputCol]) && (myCol > MinCol) && (myRow > MinRow))
                    {
                        if (myCol != InputCol)
                        {
                            NumberOfConnectedChips = NumberOfConnectedChips + 1;
                        }
                        myCol = myCol - 1;
                        myRow = myRow - 1;
                    }
                    if (((myCol == MinCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] == (Connect4Grid[myRow + 1, myCol + 1]))
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                }

                if (NumberOfConnectedChips >= 4)
                {
                    CheckConnections = true;
                    return CheckConnections;
                }
                else
                {
                    NumberOfConnectedChips = 1;
                    CheckConnections = false;
                }
                #endregion

                #region Check for ascending diagonal Connect4
                //Check for ascending diagonal Connect4

                //Is there a column on the right? Has the top row been reached?

                myRow = Row;
                myCol = InputCol;

                if ((myCol < MaxCol) && (myRow > MinRow))
                {
                    while ((Connect4Grid[myRow, myCol] == Connect4Grid[Row, InputCol]) && (myCol < MaxCol) && (myRow > MinRow))
                    {
                        if (InputCol != myCol)
                        {
                            NumberOfConnectedChips = NumberOfConnectedChips + 1;
                        }
                        myCol = myCol + 1;
                        myRow = myRow - 1;
                    }
                    if (((myCol == MaxCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] == (Connect4Grid[myRow + 1, myCol - 1]))
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                }

                //Is there a column on the left? Has the bottom row been reached?
                myRow = Row;
                myCol = InputCol;

                if ((myCol > MinCol) && (myRow < MaxRow))
                {
                    while ((Connect4Grid[myRow, myCol] == Connect4Grid[Row, InputCol]) && (myCol > MinCol) && (myRow < MaxRow))
                    {
                        if (myCol != InputCol)
                        {
                            NumberOfConnectedChips = NumberOfConnectedChips + 1;
                        }
                        myCol = myCol - 1;
                        myRow = myRow + 1;
                    }
                    if (((myCol == MinCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] == (Connect4Grid[myRow - 1, myCol + 1]))
                    {
                        NumberOfConnectedChips = NumberOfConnectedChips + 1;
                    }
                }

                if (NumberOfConnectedChips >= 4)
                {
                    CheckConnections = true;
                    return CheckConnections;
                }
                else
                {
                    NumberOfConnectedChips = 1;
                    CheckConnections = false;
                }
                #endregion
            }
            #endregion

            return CheckConnections;
        }

        //Check for possible paths to a Connect4
        private bool CheckPathToConnect4(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckPathToConnect4 = false;
            int MinRow = 1;
            int MinCol = 0;
            int MaxRow = 6;
            int MaxCol = 6;
            int myRow = Row;
            int myCol = InputCol;
            int PathToConnect4Weight = 1;
            const int _param1 = 5;
            const int _param2 = 15;
            const int _param3 = 60;

            #region Check for horizontal path to Connect4
            //Check for horizontal path to Connect4

            //Is there a column on the right?           
            if (myCol < MaxCol)
            {
                while ((Connect4Grid[Row, myCol] != PlayerTurn.Player1) &&
                       (myCol < InputCol + 4) &&
                       (myCol < MaxCol))
                {
                    switch (Connect4Grid[Row, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (myCol != InputCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol + 1;
                            if ((myCol == MinCol) && Connect4Grid[Row, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (myCol != InputCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol + 1;
                            if ((myCol == MinCol) && Connect4Grid[Row, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }

            //Is there a column on the left?
            myCol = InputCol;

            if (myCol > MinCol)
            {
                while ((Connect4Grid[Row, myCol] != PlayerTurn.Player1) &&
                       (myCol > InputCol - 4) &&
                       (myCol > MinCol))
                {
                    switch (Connect4Grid[Row, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (myCol != InputCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol - 1;
                            if ((myCol == MinCol) && Connect4Grid[Row, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (myCol != InputCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol - 1;
                            if ((myCol == MinCol) && Connect4Grid[Row, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }

            if (PathToConnect4Weight >= _param3)
            {
                CheckPathToConnect4 = true;
                return CheckPathToConnect4;
            }
            else
            {
                // PathToConnect4Weight = 1;
                CheckPathToConnect4 = false;
            }
            #endregion
            #region Check for vertical path to Connect4
            //Check for vertical path to Connect4

            //Is there at least 2 rows below the chip?
            if ((Row < 5) && (Row > 1))
            {
                while ((Connect4Grid[myRow, InputCol] == Connect4Grid[Row, InputCol]) && (myRow < MaxRow))
                {
                    if (myRow != Row)
                    {
                        PathToConnect4Weight = PathToConnect4Weight + _param2;
                    }
                    myRow = myRow + 1;
                }
                if ((myRow == MaxRow) && (Connect4Grid[myRow, InputCol] == Connect4Grid[MaxRow - 1, InputCol]))
                {
                    PathToConnect4Weight = PathToConnect4Weight + _param2;
                }
            }

            if (PathToConnect4Weight >= _param3)
            {
                CheckPathToConnect4 = true;
                return CheckPathToConnect4;
            }
            else
            {
                // PathToConnect4Weight = 1;
                CheckPathToConnect4 = false;
            }
            #endregion
            #region Check for path to diagonal Connect4
            #region Check for path to descending diagonal Connect4
            //Check for path to descending diagonal Connect4
            //Is there a column on the right? Has the bottom row been reached?

            myRow = Row;
            myCol = InputCol;

            if ((myCol < MaxCol) && (myRow < MaxRow))
            {
                while ((Connect4Grid[myRow, myCol] != PlayerTurn.Player1) &&
                       (myCol < MaxCol) &&
                       (myCol < InputCol + 4) &&
                       (myRow < MaxRow))
                {
                    switch (Connect4Grid[myRow, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol + 1;
                            myRow = myRow + 1;

                            if (((myCol == MaxCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol + 1;
                            myRow = myRow + 1;

                            if (((myCol == MaxCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }

            //Is there a column on the left? Has the top row been reached?
            myRow = Row;
            myCol = InputCol;

            if ((myCol > MinCol) && (myRow > MinRow))
            {
                while ((Connect4Grid[myRow, myCol] != PlayerTurn.Player1) &&
                       (myCol > MinCol) &&
                       (myCol > InputCol - 4) &&
                       (myRow > MinRow))
                {
                    switch (Connect4Grid[myRow, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol - 1;
                            myRow = myRow - 1;
                            if (((myCol == MinCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol - 1;
                            myRow = myRow - 1;
                            if (((myCol == MinCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }

            if (PathToConnect4Weight >= _param3)
            {
                CheckPathToConnect4 = true;
                return CheckPathToConnect4;
            }
            else
            {
                // PathToConnect4Weight = 1;
                CheckPathToConnect4 = false;
            }
            #endregion
            #region Check for path to ascending diagonal Connect4
            //Check for path to ascending diagonal Connect4

            //Is there a column on the right? Has the top row been reached?

            myRow = Row;
            myCol = InputCol;

            if ((myCol < MaxCol) && (myRow > MinRow))
            {
                while ((Connect4Grid[myRow, myCol] != PlayerTurn.Player1) &&
                       (myCol < MaxCol) &&
                       (myCol < InputCol + 4) &&
                       (myRow > MinRow))
                {
                    switch (Connect4Grid[myRow, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol + 1;
                            myRow = myRow - 1;

                            if (((myCol == MaxCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.NotPlayed)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol + 1;
                            myRow = myRow - 1;

                            if (((myCol == MaxCol) || (myRow == MinRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.NotPlayed)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }


            //Is there a column on the left? Has the bottom row been reached?
            myRow = Row;
            myCol = InputCol;

            if ((myCol > MinCol) && (myRow < MaxRow))
            {
                while ((Connect4Grid[myRow, myCol] != PlayerTurn.Player1) &&
                       (myCol > MinCol) &&
                       (myCol > InputCol - 4) &&
                       (myRow < MaxRow))
                {
                    switch (Connect4Grid[myRow, myCol])
                    {
                        case PlayerTurn.NotPlayed:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            myCol = myCol - 1;
                            myRow = myRow + 1;

                            if (((myCol == MaxCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param1;
                            }
                            break;
                        case PlayerTurn.Player2:
                            if (InputCol != myCol)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            myCol = myCol - 1;
                            myRow = myRow + 1;

                            if (((myCol == MaxCol) || (myRow == MaxRow)) && Connect4Grid[myRow, myCol] != PlayerTurn.Player1)
                            {
                                PathToConnect4Weight = PathToConnect4Weight + _param2;
                            }
                            break;
                    }
                }
            }

            if (PathToConnect4Weight >= _param3)
            {
                CheckPathToConnect4 = true;
                return CheckPathToConnect4;
            }
            else
            {
                PathToConnect4Weight = 1;
                CheckPathToConnect4 = false;
            }
            #endregion

            #endregion

            return CheckPathToConnect4;
        }

        //Logic to play against the computer
        private void FindBestMove()
        {
            //Attack
            //Check for potential winning Connect4
            if (CheckWinningConnect3(Connect4Grid, Row, InputCol) == true)
            {
                return;
            }

            //On first move, play middle column if adverse move was in either column 0, 1, 5 or 6
            if ((MoveCounter == 1) && (InitiatePotentialWinningConnect2(Connect4Grid, InputCol) == true))
            {
                return;
            }

            //Defense
            //Check for potential adverse Connect4
            if (CheckAdverseConnect3(Connect4Grid, Row, InputCol) == true)
            {
                return;
            }

            //Check for potential adverse horizontal Connect2 with two open ends
            if (CheckAdverseConnect2(Connect4Grid, Row, InputCol) == true)
            {
                return;
            }

            //Attack
            //Check for potential horizontal Connect2 with two open ends
            if (CheckWinningConnect2(Connect4Grid, Row, InputCol) == true)
            {
                return;
            }

            //All previous checks returned false, play the same column as the other player as long as the column isn't full
            if (CheckFriendlyConnects(Connect4Grid, Row, InputCol) == true)
            {
                return;
            }

        }

        //Defense
        //Check for potential adverse Connect4
        private Boolean CheckAdverseConnect3(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckAdverseConnect3 = false;
            int MaxCol = 6;
            int myRow = Row;
            int myCol = InputCol;

            #region Check for potential adverse Connect4
            //Check for potential adverse Connect4
            //Test each column and see if the other player would get a Connect4 by selecting it.
            //If yes, play that column to prevent the other player from winning on the next move.
            for (myCol = 0; myCol <= MaxCol; myCol++)
            {
                myRow = GetRowNumber(myCol);
                if (myRow > 0)
                {
                    Connect4Grid[myRow, myCol] = PlayerTurn.Player1;
                    if (CheckConnections(Connect4Grid, myRow, myCol) == true)
                    {
                        Connect4Grid[myRow, myCol] = PlayerTurn.NotPlayed;
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                        CheckAdverseConnect3 = true;
                        return CheckAdverseConnect3;
                    }
                    else
                    {
                        Connect4Grid[myRow, myCol] = PlayerTurn.NotPlayed;
                        CheckAdverseConnect3 = false;
                    }
                }
                else
                {
                    CheckAdverseConnect3 = false;
                    //break;
                }
            }
            #endregion

            return CheckAdverseConnect3;
        }

        #region //Check for potential adverse horizontal Connect2 with two open ends
        //Check for potential adverse horizontal Connect2 with two open ends
        private Boolean CheckAdverseConnect2(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            //If 1 < InputCol < 6 Check if there is an identical adverse chip on the right
            //and an empty cell next to it which can be played on the next turn (it's either on the bottom row
            //or a chip exists in the lower row for that column).
            //If this is the case, check if an empty cell on the left exists and if it can be played on the next turn
            //(it's either on the bottom row or a chip exists in the lower row for that column)
            bool CheckAdverseConnect2 = false;
            int MaxRow = 6;

            # region adverse chip on the right
            if ((InputCol > 0) && (InputCol < 5) &&
                (Connect4Grid[Row, InputCol] == Connect4Grid[Row, InputCol + 1]) &&
                (Connect4Grid[Row, InputCol - 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol + 2] == PlayerTurn.NotPlayed))
            {
                if (Row == MaxRow)
                {
                    if (InputCol < 3)
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 2);
                    }
                    else
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 1);
                    }
                    CheckAdverseConnect2 = true;
                    return CheckAdverseConnect2;
                }
                else
                {
                    if ((Connect4Grid[Row + 1, InputCol - 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol + 2] != PlayerTurn.NotPlayed))
                    {
                        if (InputCol < 3)
                        {
                            MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 2);
                        }
                        else
                        {
                            MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 1);
                        }
                        CheckAdverseConnect2 = true;
                        return CheckAdverseConnect2;
                    }
                    else
                    {
                        CheckAdverseConnect2 = false;
                    }
                }
            }
            else
            {
                CheckAdverseConnect2 = false;
            }
            #endregion
            //Do the same thing for 2 < InputCol < 7 on the left this time
            #region adverse chip on the left
            if ((InputCol > 1) && (InputCol < 6) &&
                (Connect4Grid[Row, InputCol] == Connect4Grid[Row, InputCol - 1]) &&
                (Connect4Grid[Row, InputCol + 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol - 2] == PlayerTurn.NotPlayed))
            {
                if (Row == MaxRow)
                {
                    if (InputCol < 4)
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 1);
                    }
                    else
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 2);
                    }
                    CheckAdverseConnect2 = true;
                    return CheckAdverseConnect2;
                }
                else
                {
                    if ((Connect4Grid[Row + 1, InputCol + 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol - 2] != PlayerTurn.NotPlayed))
                    {
                        if (InputCol < 4)
                        {
                            MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 1);
                        }
                        else
                        {
                            MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 2);
                        }
                        CheckAdverseConnect2 = true;
                        return CheckAdverseConnect2;
                    }
                    else
                    {
                        CheckAdverseConnect2 = false;
                    }
                }
            }
            else
            {
                CheckAdverseConnect2 = false;
            }
            #endregion

            //Check if there is an empty cell followed by an identical adverse chip on the right 
            //and an empty cell next to it which can be played on the next turn (it's either on the bottom row
            //or a chip exists in the lower row for that column).
            //If this is the case, check if an empty cell on the left exists and if it can be played on the next turn
            //(it's either on the bottom row or a chip exists in the lower row for that column)

            #region empty cell followed by an identical adverse chip on the right
            if ((InputCol > 0) && (InputCol < 4) &&
                (Connect4Grid[Row, InputCol] == Connect4Grid[Row, InputCol + 2]) &&
                (Connect4Grid[Row, InputCol - 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol + 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol + 3] == PlayerTurn.NotPlayed))
            {
                if (Row == MaxRow)
                {
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 1);
                    CheckAdverseConnect2 = true;
                    return CheckAdverseConnect2;
                }
                else
                {
                    if ((Connect4Grid[Row + 1, InputCol - 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol + 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol + 3] != PlayerTurn.NotPlayed))
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol + 1);
                        CheckAdverseConnect2 = true;
                        return CheckAdverseConnect2;
                    }
                    else
                    {
                        CheckAdverseConnect2 = false;
                    }
                }
            }
            else
            {
                CheckAdverseConnect2 = false;
            }
            #endregion
            //Do the same thing for 2 < InputCol < 7 on the left this time
            #region empty cell followed by an identical adverse chip on the left
            if ((InputCol > 2) && (InputCol < 6) &&
                (Connect4Grid[Row, InputCol] == Connect4Grid[Row, InputCol - 2]) &&
                (Connect4Grid[Row, InputCol + 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol - 1] == PlayerTurn.NotPlayed) &&
                (Connect4Grid[Row, InputCol - 3] == PlayerTurn.NotPlayed))
            {
                if (Row == MaxRow)
                {
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 1);
                    CheckAdverseConnect2 = true;
                    return CheckAdverseConnect2;
                }
                else
                {
                    if ((Connect4Grid[Row + 1, InputCol + 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol - 1] != PlayerTurn.NotPlayed) &&
                        (Connect4Grid[Row + 1, InputCol - 3] != PlayerTurn.NotPlayed))
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol - 1);
                        CheckAdverseConnect2 = true;
                        return CheckAdverseConnect2;
                    }
                    else
                    {
                        CheckAdverseConnect2 = false;
                    }
                }
            }
            else
            {
                CheckAdverseConnect2 = false;
            }
            #endregion
            return CheckAdverseConnect2;
        }

        #endregion
        //Check for potential adverse Connect4 on top of my selected column
        private Boolean CheckPostMoveAdverseConnect3(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckPostMoveAdverseConnect3 = false;
            int MaxCol = 6;
            int myRow = Row;
            int myCol = InputCol;

            myRow = GetRowNumber(myCol);
            if (myRow > 1)
            {
                Connect4Grid[myRow - 1, myCol] = PlayerTurn.Player1;
                if (CheckConnections(Connect4Grid, myRow - 1, myCol) == true)
                {
                    Connect4Grid[myRow - 1, myCol] = PlayerTurn.NotPlayed;
                    CheckPostMoveAdverseConnect3 = true;
                }
                else
                {
                    Connect4Grid[myRow - 1, myCol] = PlayerTurn.NotPlayed;
                }
            }
            return CheckPostMoveAdverseConnect3;
        }

        //Attack
        //Check for potential winning Connect4    
        private Boolean CheckWinningConnect3(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckWinningConnect3 = false;
            int MaxCol = 6;
            int myRow = Row;
            int myCol = InputCol;

            #region Check for potential winning Connect4
            //Test each column and see if the computer would get a Connect4 by selecting it.
            //If yes, play that column to win on that move.   

            for (myCol = 0; myCol <= MaxCol; myCol++)
            {
                myRow = GetRowNumber(myCol);
                if (myRow > 0)
                {
                    Connect4Grid[myRow, myCol] = PlayerTurn.Player2;
                    if (CheckConnections(Connect4Grid, myRow, myCol) == true)
                    {
                        Connect4Grid[myRow, myCol] = PlayerTurn.NotPlayed;
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                        CheckWinningConnect3 = true;
                        //MessageBox.Show("Winning on CheckWinningConnect3");
                        return CheckWinningConnect3;
                    }
                    else
                    {
                        Connect4Grid[myRow, myCol] = PlayerTurn.NotPlayed;
                    }
                }
            }
            #endregion
            return CheckWinningConnect3;
        }

        //Check for initiating potential horizontal Connect2 with two open ends
        private Boolean InitiatePotentialWinningConnect2(PlayerTurn[,] Connect4Grid, int InputCol)
        {
            //On second move, play middle column if adverse move was in either column 0, 1, 5 or 6
            bool InitiatePotentialWinningConnect2 = false;
            int myCol = 0;
            int lower = 2;
            int upper = 5;
            Random randCol = new Random();

            switch (InputCol)
            {
                case 0:
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, 3);
                    break;
                case 1:
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, 4);
                    break;
                case 2:
                    myCol = randCol.Next(lower, upper);
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                    break;
                case 3:
                    myCol = randCol.Next(lower, upper);
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                    break;
                case 4:
                    myCol = randCol.Next(lower, upper);
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                    break;
                case 5:
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, 2);
                    break;
                case 6:
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, 3);
                    break;
            }
            InitiatePotentialWinningConnect2 = true;
            return InitiatePotentialWinningConnect2;
        }

        //Check for potential horizontal Connect2 with two open ends
        private Boolean CheckWinningConnect2(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckWinningConnect2 = false;
            int myRow = 0;
            int maxRow = 6;
            int myCol = 0;

            for (myCol = 1; myCol < 4; myCol++)
            {
                myRow = GetRowNumber(myCol);
                if ((Connect4Grid[myRow, myCol + 1] == PlayerTurn.Player2) &&
                    (Connect4Grid[myRow, myCol + 1] == Connect4Grid[myRow, myCol + 2]) &&
                    (Connect4Grid[myRow, myCol + 3] == PlayerTurn.NotPlayed))
                {
                    if (Connect4Grid[myRow, myCol - 1] == PlayerTurn.NotPlayed)
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol);
                        CheckWinningConnect2 = true;
                        return CheckWinningConnect2;
                    }
                    if (Connect4Grid[myRow, myCol + 4] == PlayerTurn.NotPlayed)
                    {
                        MakeAMove(PlayerTurn.Player2, Connect4Grid, myCol + 3);
                        CheckWinningConnect2 = true;
                        return CheckWinningConnect2;
                    }
                }
            }
            return CheckWinningConnect2;
        }

        //Check for potential Connect1s and Connect2s
        private Boolean CheckFriendlyConnects(PlayerTurn[,] Connect4Grid, int Row, int InputCol)
        {
            bool CheckFriendlyConnects = false;
            int myRow = 0;
            int maxRow = 6;
            int myCol = 0;
            //int myColWeight = 0;
            int myTempColWeight = 0;
            int myDeepRow = 0;

            for (myCol = 1; myCol < 6; myCol++)
            {
                //myTempColWeight = 0;
                myRow = GetRowNumber(myCol);

                #region old algorithm
                //// Check friendly connect in the same column
                //// if the column is full, the column weight is set to 0
                //if (myRow == 0)
                //{
                //    myTempColWeight = 0;
                //}
                //// if the column isn't full, the weight is set to 3 if the chip below is a connected chip
                //// and 2 if the chip is an adverse chip.
                //if ((myRow > 0) && (myRow < maxRow))
                //{         
                //    #region North East Friendly Connects
                //    if (myRow > 1)
                //    {
                //        switch (Connect4Grid[myRow - 1, myCol + 1])
                //        {
                //            case PlayerTurn.NotPlayed:
                //                if (Connect4Grid[myRow, myCol + 1] != PlayerTurn.NotPlayed)
                //                {
                //                    myTempColWeight = myTempColWeight + 2;
                //                    break;
                //                }
                //                else
                //                {
                //                    myTempColWeight = myTempColWeight + 1;
                //                    break;
                //                }

                //            case PlayerTurn.Player1:
                //                myTempColWeight = myTempColWeight + 4;
                //                break;

                //            case PlayerTurn.Player2:
                //                myTempColWeight = myTempColWeight + 8;
                //                if ((myRow > 2) && (myCol < 5))
                //                    {
                //                        switch (Connect4Grid[myRow - 2, myCol + 1])
                //                        {
                //                            case PlayerTurn.NotPlayed:
                //                                if (Connect4Grid[myRow - 1, myCol + 2] != PlayerTurn.NotPlayed)
                //                                {
                //                                    myTempColWeight = myTempColWeight + 2;
                //                                    break;
                //                                }
                //                                else
                //                                {
                //                                    myTempColWeight = myTempColWeight + 1;
                //                                    break;
                //                                }

                //                           case PlayerTurn.Player1:
                //                                    break;
                //                           case PlayerTurn.Player2:
                //                                    break;
                //                        }

                //                    }
                //                break;
                //        }
                //    }
                //    if ((myRow > 2) && (myCol < 5))
                //    {
                //        switch (Connect4Grid[myRow - 2, myCol + 2])
                //        {
                //            case PlayerTurn.NotPlayed:
                //                if (Connect4Grid[myRow - 1, myCol + 1] != PlayerTurn.NotPlayed)
                //                {
                //                    myTempColWeight = myTempColWeight + 2;
                //                    break;
                //                }
                //                else
                //                {
                //                    myTempColWeight = myTempColWeight + 1;
                //                    break;
                //                }

                //            case PlayerTurn.Player1:
                //                myTempColWeight = myTempColWeight + 4;
                //                break;

                //            case PlayerTurn.Player2:
                //                myTempColWeight = myTempColWeight + 8;
                //                break;
                //        }
                //    }
                //    #endregion
                //    #region East Friendly Connects
                //    switch (Connect4Grid[myRow, myCol + 1])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            if (Connect4Grid[myRow + 1, myCol + 1] != PlayerTurn.NotPlayed)
                //            {
                //                myTempColWeight = myTempColWeight + 2;
                //                break;
                //            }
                //            else
                //            {
                //                myTempColWeight = myTempColWeight + 1;
                //                break;
                //            }

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4;
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //    #endregion                                      
                //        #region South Friendly Connects
                //        if (Connect4Grid[myRow + 1, myCol] == Connect4Grid[myRow, myCol])
                //        {
                //            myTempColWeight = 6;
                //        }
                //        else
                //        {
                //            myTempColWeight = 4;
                //        }
                //        #endregion                    
                //        #region South East Friendly Connects
                //        switch (Connect4Grid[myRow + 1, myCol + 1])
                //        {
                //            case PlayerTurn.NotPlayed:
                //                if (myRow < 4)
                //                {
                //                    if (Connect4Grid[myRow + 2, myCol + 1] != PlayerTurn.NotPlayed)
                //                    {
                //                        myTempColWeight = myTempColWeight + 2;
                //                        break;
                //                    }
                //                    else
                //                    {
                //                        myTempColWeight = myTempColWeight + 1;
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    myTempColWeight = myTempColWeight + 1;
                //                    break;
                //                }

                //            case PlayerTurn.Player1:
                //                myTempColWeight = myTempColWeight + 4; 
                //                break;

                //            case PlayerTurn.Player2:
                //                myTempColWeight = myTempColWeight + 8;
                //                break;
                //        }
                //        #endregion
                //        #region South West Friendly Connects
                //        switch (Connect4Grid[myRow + 1, myCol - 1])
                //        {
                //            case PlayerTurn.NotPlayed:
                //                if (myRow < 4)
                //                {
                //                    if (Connect4Grid[myRow + 2, myCol - 1] != PlayerTurn.NotPlayed)
                //                    {
                //                        myTempColWeight = myTempColWeight + 2;
                //                        break;
                //                    }
                //                    else
                //                    {
                //                        myTempColWeight = myTempColWeight + 1;
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    myTempColWeight = myTempColWeight + 1;
                //                    break;
                //                }

                //            case PlayerTurn.Player1:
                //                myTempColWeight = myTempColWeight + 4; 
                //                break;

                //            case PlayerTurn.Player2:
                //                myTempColWeight = myTempColWeight + 8;
                //                break;
                //        }
                //        #endregion                                     
                //    #region North West Friendly Connects
                //        if (myRow > 1)
                //        {
                //            switch (Connect4Grid[myRow - 1, myCol - 1])
                //            {
                //                case PlayerTurn.NotPlayed:
                //                    if (Connect4Grid[myRow, myCol - 1] != PlayerTurn.NotPlayed)
                //                    {
                //                        myTempColWeight = myTempColWeight + 2;
                //                        break;
                //                    }
                //                    else
                //                    {
                //                        myTempColWeight = myTempColWeight + 1;
                //                        break;
                //                    }

                //                case PlayerTurn.Player1:
                //                    myTempColWeight = myTempColWeight + 4;
                //                    break;

                //                case PlayerTurn.Player2:
                //                    myTempColWeight = myTempColWeight + 8;
                //                    break;
                //            }
                //        }
                //    #endregion
                //    #region West Friendly Connects
                //    switch (Connect4Grid[myRow, myCol -1 ])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            if (Connect4Grid[myRow + 1, myCol - 1] != PlayerTurn.NotPlayed)
                //            {
                //                myTempColWeight = myTempColWeight + 2;
                //                break;
                //            }
                //            else
                //            {
                //                myTempColWeight = myTempColWeight + 1;
                //                break;
                //            }

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4; 
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //    #endregion              
                //}

                //if (myRow == maxRow)
                //{
                //    #region North East Friendly Connects

                //        switch (Connect4Grid[myRow - 1, myCol + 1])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            if (Connect4Grid[myRow, myCol + 1] != PlayerTurn.NotPlayed)
                //            {
                //                myTempColWeight = myTempColWeight + 2;
                //                break;
                //            }
                //            else
                //            {
                //                myTempColWeight = myTempColWeight + 1;
                //                break;
                //            }

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4;
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //    #endregion
                //    #region East Friendly Connects
                //    switch (Connect4Grid[myRow, myCol + 1])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            myTempColWeight = myTempColWeight + 2;
                //            break;

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4;
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //    #endregion
                //    #region North West Friendly Connects
                //    switch (Connect4Grid[myRow - 1, myCol - 1])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            if (Connect4Grid[myRow, myCol - 1] != PlayerTurn.NotPlayed)
                //            {
                //                myTempColWeight = myTempColWeight + 2;
                //                break;
                //            }
                //            else
                //            {
                //                myTempColWeight = myTempColWeight + 1;
                //                break;
                //            }

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4;
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //                        #endregion
                //    #region West Friendly Connects
                //    switch (Connect4Grid[myRow, myCol - 1])
                //    {
                //        case PlayerTurn.NotPlayed:
                //            myTempColWeight = myTempColWeight + 1;
                //            break;

                //        case PlayerTurn.Player1:
                //            myTempColWeight = myTempColWeight + 4;
                //            break;

                //        case PlayerTurn.Player2:
                //            myTempColWeight = myTempColWeight + 8;
                //            break;
                //    }
                //    #endregion              
                //}

                //if (CheckPostMoveAdverseConnect3(Connect4Grid, myRow, myCol))
                //{
                //    myTempColWeight = 0;
                //}

                //if ((myTempColWeight > myColWeight) && (GetRowNumber(myCol) > 0))
                //{
                //    InputCol = myCol;
                //    myColWeight = myTempColWeight;
                //}

                #endregion

                if (CheckPathToConnect4(Connect4Grid, myRow, myCol))
                {
                    if (CheckPostMoveAdverseConnect3(Connect4Grid, myRow, myCol))
                        break;
                    else
                    {
                        InputCol = myCol;
                    }
                }
            }

            if (MoveCounter < 42)
            {
                if (GetRowNumber(InputCol) > 0)
                {
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol);
                }
                else
                {
                    for (myCol = 0; myCol <= 6; myCol++)
                    {
                        myDeepRow = GetRowNumber(myCol);
                        if (myDeepRow > 0)
                        {
                            myRow = myDeepRow;
                            InputCol = myCol;
                        }
                    }
                    MakeAMove(PlayerTurn.Player2, Connect4Grid, InputCol);
                }
            }
            return CheckFriendlyConnects;
        }

        //Determine player turn
        private PlayerTurn GetPlayerTurn(PlayerTurn Player)
        {

            if (Player == Window1.PlayerTurn.Player1)
            {
                return Window1.PlayerTurn.Player2;
            }
            else
            {
                return Window1.PlayerTurn.Player1;
            }

        }

        enum PlayerTurn
        {
            NotPlayed,
            Player1,
            Player2
        };

        private void OnTwoPlayers(object sender, RoutedEventArgs e)
        {

        }

        //Connect4 happened. Game is over
        private Boolean GameOver(bool CheckConnections)
        {
            bool GameOver = false;

            if (CheckConnections == true)
            {
                GameOver = true;
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;
                button4.IsEnabled = false;
                button5.IsEnabled = false;
                button6.IsEnabled = false;
                button7.IsEnabled = false;
            }
            return GameOver;
        }

    }
}
