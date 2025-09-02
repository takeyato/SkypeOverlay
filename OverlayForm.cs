using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class OverlayForm : Form
{
        [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

                [DllImport("user32.dll")]
                    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

                        [DllImport("user32.dll")]
                            static extern int GetWindowLong(IntPtr hWnd, int nIndex);

                                [DllImport("user32.dll")]
                                    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

                                        const int GWL_EXSTYLE = -20;
                                            const int WS_EX_LAYERED = 0x80000;
                                                const int WS_EX_TRANSPARENT = 0x20;
                                                    const int WS_EX_NOACTIVATE = 0x8000000;

                                                        [StructLayout(LayoutKind.Sequential)]
                                                            public struct RECT
                                                                {
                                                                            public int Left, Top, Right, Bottom;
                                                                }

                                                                    Timer positionTimer;

                                                                        public OverlayForm()
                                                                            {
                                                                                        this.FormBorderStyle = FormBorderStyle.None;
                                                                                                this.TopMost = true;
                                                                                                        this.ShowInTaskbar = false;
                                                                                                                this.BackColor = Color.Black;
                                                                                                                        this.Opacity = 0.6; // 黒背景の透過度
                                                                                                                                this.DoubleBuffered = true;

                                                                                                                                        this.Load += (s, e) =>
                                                                                                                                                {
                                                                                                                                                                int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
                                                                                                                                                                            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE);
                                                                                                                                                };

                                                                                                                                                        positionTimer = new Timer();
                                                                                                                                                                positionTimer.Interval = 1000; // 1秒ごとに位置更新
                                                                                                                                                                        positionTimer.Tick += (s, e) => UpdateOverlayPosition();
                                                                                                                                                                                positionTimer.Start();
                                                                            }

                                                                                protected override void OnPaint(PaintEventArgs e)
                                                                                    {
                                                                                                using (Brush purpleOverlay = new SolidBrush(Color.FromArgb(80, 128, 0, 128))) // 紫の半透明
                                                                                                        {
                                                                                                                        e.Graphics.FillRectangle(purpleOverlay, this.ClientRectangle);
                                                                                                        }
                                                                                    }

                                                                                        void UpdateOverlayPosition()
                                                                                            {
                                                                                                        IntPtr skypeHandle = FindWindow(null, "Skype"); // ウィンドウタイトルが "Skype" の場合
                                                                                                                if (skypeHandle == IntPtr.Zero) return;

                                                                                                                        if (GetWindowRect(skypeHandle, out RECT rect))
                                                                                                                                {
                                                                                                                                                this.Location = new Point(rect.Left, rect.Top);
                                                                                                                                                            this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                                                                                                                                }
                                                                                            }
}
                                                                                                                                }
                                                                                            }
                                                                                                        }
                                                                                    }
                                                                                                                                                }
                                                                            }
                                                                }
}