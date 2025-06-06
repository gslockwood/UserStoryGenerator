using System.Diagnostics;
using System.Drawing.Drawing2D;
namespace UserStoryGenerator.View
{
    public partial class StopwatchClock : UserControl, IReset
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private System.Windows.Forms.Timer _timer = new();

        private Color _clockFaceColor = SystemColors.Control;
        private Color _secondHandColor = Color.Red;
        private Color _centerDotColor = Color.Black;
        private Color _tickMarkColor = Color.Gray;
        public StopwatchClock()
        {
            InitializeComponent();

            // Set up the timer for redrawing
            _timer.Interval = 50; // Update every 50 milliseconds for smooth movement
            _timer.Tick += Timer_Tick;

            // Enable double buffering for smoother drawing
            this.DoubleBuffered = true;
            this.Size = new Size(200, 200); // Default size
        }

        #region Public Properties

        public Color ClockFaceColor
        {
            get { return _clockFaceColor; }
            set
            {
                _clockFaceColor = value;
                this.Invalidate();
            }
        }

        public Color SecondHandColor
        {
            get { return _secondHandColor; }
            set
            {
                _secondHandColor = value;
                this.Invalidate();
            }
        }

        public bool HasCenterDot
        {
            get { return this.hasCenterDot; }
            set { this.hasCenterDot = value; }
        }
        public bool IsTicked
        {
            get { return this.isTicked; }
            set { this.isTicked = value; }
        }

        public Color CenterDotColor
        {
            get { return _centerDotColor; }
            set
            {
                _centerDotColor = value;
                this.Invalidate();
            }
        }

        public Color TickMarkColor
        {
            get { return _tickMarkColor; }
            set
            {
                _tickMarkColor = value;
                this.Invalidate();
            }
        }

        public bool IsRunning
        {
            get { return _stopwatch.IsRunning; }
        }

        public TimeSpan ElapsedTime
        {
            get { return _stopwatch.Elapsed; }
        }

        #endregion

        #region Stopwatch Methods

        public void Start()
        {
            if( !_stopwatch.IsRunning )
            {
                Reset();
                _stopwatch.Start();
                _timer.Start();
                OnStopwatchStarted(EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if( _stopwatch.IsRunning )
            {
                _stopwatch.Stop();
                _timer.Stop();
                // Pass the specific EventArgs when calling OnStopwatchStopped
                OnStopwatchStopped(new StopwatchStoppedEventArgs(_stopwatch.Elapsed));
            }
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _timer.Stop();
            this.Invalidate();
            OnStopwatchReset(EventArgs.Empty);
        }

        #endregion

        #region Drawing
        bool isTicked = true;
        bool hasCenterDot = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = Width / 2;
            int centerY = Height / 2;
            int radius = Math.Min(Width, Height) / 2 - 5;

            using( Brush clockBrush = new SolidBrush(_clockFaceColor) )
            {
                g.FillEllipse(clockBrush, centerX - radius, centerY - radius, radius * 2, radius * 2);
            }
            using( Pen clockPen = new Pen(Color.DarkGray, 2) )
            {
                g.DrawEllipse(clockPen, centerX - radius, centerY - radius, radius * 2, radius * 2);
            }

            if( isTicked )
            {
                int lengthOfTick = 6;//10
                using( Pen tickPen = new Pen(_tickMarkColor, 1) )
                {
                    for( int i = 0; i < 60; i += 5 )
                    {
                        if( i % 3 == 0 )
                            lengthOfTick = 10;
                        else
                            lengthOfTick = 6;

                        double angle = ( i * 6 - 90 ) * Math.PI / 180;
                        float x1 = centerX + (float)( ( radius - lengthOfTick ) * Math.Cos(angle) );
                        float y1 = centerY + (float)( ( radius - lengthOfTick ) * Math.Sin(angle) );
                        float x2 = centerX + (float)( radius * Math.Cos(angle) );
                        float y2 = centerY + (float)( radius * Math.Sin(angle) );
                        g.DrawLine(tickPen, x1, y1, x2, y2);
                    }
                }
            }

            double totalSeconds = _stopwatch.Elapsed.TotalSeconds;
            double secondAngle = ( totalSeconds * 6 - 90 ) * Math.PI / 180;

            using( Pen secondHandPen = new Pen(_secondHandColor, 2) )
            {
                secondHandPen.StartCap = LineCap.Round;
                secondHandPen.EndCap = LineCap.ArrowAnchor;

                float handLength = radius * 0.8f;
                float x = centerX + (float)( handLength * Math.Cos(secondAngle) );
                float y = centerY + (float)( handLength * Math.Sin(secondAngle) );

                g.DrawLine(secondHandPen, centerX, centerY, x, y);
            }
            if( hasCenterDot )
            {
                int dotSize = 4;//8
                using( Brush centerBrush = new SolidBrush(_centerDotColor) )
                {
                    g.FillEllipse(centerBrush, centerX - dotSize / 2, centerY - dotSize / 2, dotSize, dotSize);
                }
            }
        }

        #endregion

        #region Event Handlers

        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region Custom Events

        public event EventHandler? StopwatchStarted;
        public event EventHandler<StopwatchStoppedEventArgs>? StopwatchStopped;
        public event EventHandler? StopwatchReset;
        protected virtual void OnStopwatchStarted(EventArgs e)
        {
            StopwatchStarted?.Invoke(this, e);
        }

        // Changed to use StopwatchStoppedEventArgs
        protected virtual void OnStopwatchStopped(StopwatchStoppedEventArgs e) // Changed parameter type
        {
            StopwatchStopped?.Invoke(this, e);
        }

        protected virtual void OnStopwatchReset(EventArgs e)
        {
            StopwatchReset?.Invoke(this, e);
        }

        #endregion

    }

    public class StopwatchStoppedEventArgs : EventArgs
    {
        public TimeSpan ElapsedTime { get; private set; }

        public StopwatchStoppedEventArgs(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }
    }

}
