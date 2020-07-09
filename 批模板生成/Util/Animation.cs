using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace 批模板生成.Util
{
	public class AnimationStamp
	{
		private int interval;
		private int delay;
		private int startTime;
		public AnimationStamp(int interval,int delay)
		{
			this.interval = interval;
			Start(delay);
		}

		public void Start() => Start(delay);
		public void Start(int delay)
		{
			startTime = Environment.TickCount;
			this.delay = delay;
		}

		public double NowStamp()
		{
			int trueStamp = Environment.TickCount - startTime - delay;
			if (trueStamp < 0) return 0;
			if (trueStamp > interval) return 1;
			return Math.Pow((double)trueStamp / (double)interval,0.5);
		}

	}
}
