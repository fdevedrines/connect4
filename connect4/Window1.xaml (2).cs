using System;
using System.Collections.Generic;
using System.Text;
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
        int Row = -1;
        int Player1Score = 0;
        int Player2Score = 0;

        PlayerTurn[,] Connect4Grid = new PlayerTurn[6, 7];
        PlayerTurn Player = PlayerTurn.NotPlayed;   

        public Window1()
        {
            InitializeComponent();
            //Draw Ellipses to reflect Grid holes
            DrawGridHoles();
            // Centralize handling of all clicks in the Connect4Grid.
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnButtonClick));
        }

        //Define DependencyProperty
        public static DependencyProperty SelectedCol;

        static Window1()
        {
            SelectedCol = DependencyProperty.Register("InputCol", typeof(int),typeof(Window1),
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

            if (b.Name == "reset")
            {
                //Connect4 is reset
                InitializeComponent();
                Connect4Grid = new PlayerTurn[6, 7];
                //Clear objects in Grid
                myGrid.Children.Clear();
                //Draw Ellipses to reflect Grid holes
                DrawGridHoles();
                //Ensure all buttons are enabled
                button1.IsEnabled = true;
                button2.IsEnabled = true;
                button3.IsEnabled = true;
                button4.IsEnabled = true;
                button5.IsEnabled = true;
                button6.IsEnabled = true;
                button7.IsEnabled = true;
            }
            else
            //Figure out which column was selected for the next move
            {
                #region Button Selected

                if (b.Name == "button1")
                {
                    InputCol = 0;
                }
                if (b.Name == "button2")
                {
                    InputCol = 1;
                }
                if (b.Name == "button3")
                {
                    InputCol = 2;
                }
                if (b.Name == "button4")
                {
                    InputCol = 3;
                }
                if (b.Name == "button5")
                {
                    InputCol = 4;
                }
                if (b.Name == "button6")
                {
                    InputCol = 5;
                }
                if (b.Name == "button7")
                {
                    InputCol = 6;
                }
                #endregion
                Player = GetPlayerTurn(Player);
                Row = GetRowNumber(InputCol);
                Connect4State(Connect4Grid, Row, InputCol);
                MoveCounter++;
                if (MoveCounter >= 7)
                {
                    CheckConnections(Connect4Grid, Row, InputCol);
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

        //Functions
        //Identify the Row number the chip will fall in
        private int GetRowNumber(int InputCol)
        {
            int myRow = 5;

            if (Connect4Grid[myRow, InputCol] == PlayerTurn.NotPlayed)
            {
                return myRow;
            }
            else
            {
                while (Connect4Grid[myRow, InputCol] != PlayerTurn.NotPlayed)
                {
                    myRow--;
                    //Disable button when column is full
                    #region Disable buttons
                    if (myRow == 0)
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
                    }
                    #endregion
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

            Grid.SetRow(myEllipse, Row);
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
            int MinRow = 0;
            int MinCol = 0;
            int MaxRow = 5;
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
                MessageBox.Show("Game Over");
                return CheckConnections;
            }
            else
            {
                NumberOfConnectedChips = 1;
                CheckConnections = false;
                //return CheckConnections;
            }
            #endregion

            #region Check for vertical Connect4
            //Check for vertical Connect4

            //Is there at least 3 rows below the chip?
            if (Row <= 3)
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
                MessageBox.Show("Game Over");
                return CheckConnections;
            }
            else
            {
                NumberOfConnectedChips = 1;
                CheckConnections = false;
            }
            #endregion

            #region Check for diagonal Connect4
            //A diagonal Connect4 is possible only after a minimum of 11 moves

            if (MoveCounter < 11)
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
                    MessageBox.Show("Game Over");
                    return CheckConnections;
                }
                else
                {
                    NumberOfConnectedChips = 1;
                    CheckConnections = false;
                    //return CheckConnections;
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
                    MessageBox.Show("Game Over");
                    return CheckConnections;
                }
                else
                {
                    NumberOfConnectedChips = 1;
                    CheckConnections = false;
                    return CheckConnections;
                }
            #endregion
            }
            #endregion
        }
        //Logic to play against the computer

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

    }
}
