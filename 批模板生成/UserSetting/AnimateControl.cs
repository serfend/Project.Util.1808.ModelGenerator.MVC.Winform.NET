using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 批模板生成.Util;

namespace 批模板生成.UserSetting
{
	public class AnimateControl:Control
	{
		public AnimateControl()
		{
			bckBrush=new SolidBrush(BackColor);
			foreBrush=new SolidBrush(ForeColor);
		}
		protected RectangleF TargetPos;
		protected RectangleF PreviousPos;

		protected Color TargetColor;
		protected Color PreviousColor;
		protected AnimationStamp animator;
		public virtual void Animate(int interval,int delay,RectangleF target,Color bckColorTarget)
		{
			PreviousPos = this.Bounds;
			PreviousColor = this.BackColor;
			TargetPos = target;
			TargetColor = bckColorTarget;
			animator=new AnimationStamp(interval,delay);
			Program.AnimationOn = true;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>当动画已结束，返回<code>true</code></returns>
		public virtual bool RefreshLayout()
		{
			if (animator == null) return true;
			double stamp = animator.NowStamp();
			double leftTime = 1 - stamp;
			bool noRefresh = Math.Abs(stamp - 1) < 0.001;
			
			this.Invoke((EventHandler) delegate
			{
				Left = (int) (TargetPos.X * stamp + PreviousPos.X * leftTime);
				Top = (int) (TargetPos.Y * stamp + PreviousPos.Y * leftTime);
				Width = (int) (TargetPos.Width * stamp + PreviousPos.Width * leftTime);
				Height = (int) (TargetPos.Height * stamp + PreviousPos.Height * leftTime);
				BackColor =Color.FromArgb(
					(int) (TargetColor.A * stamp + PreviousColor.A * leftTime),
					(int) (TargetColor.R * stamp + PreviousColor.R * leftTime),
					(int) (TargetColor.G * stamp + PreviousColor.G * leftTime),
					(int) (TargetColor.B * stamp + PreviousColor.B * leftTime)
					) ;
			});
			Invalidate();
			if (noRefresh) animator = null;
			return noRefresh;
		}


		protected readonly SolidBrush foreBrush;
		protected readonly SolidBrush bckBrush;


		protected override void OnForeColorChanged(EventArgs e)
		{
			this.foreBrush.Color = ForeColor;
		}
		
		private Color bckColor=Color.White;
		public Color BackColor { get=>bckColor;
			set
			{
				bckColor = value;
				this.bckBrush.Color = BackColor;
			}
		}
	}
}
