using System.Collections.Generic;
using System.Text;

namespace Iql
{
    public class IqlTimeSpanExpression : IqlExpression
    {
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public int? Seconds { get; set; }
        public int? Milliseconds { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            sb.Append("P");
            if (Days.HasValue)
            {
                sb.Append($"{Days}D");
            }
            if (Hours.HasValue || Minutes.HasValue || Seconds.HasValue || Milliseconds.HasValue)
            {
                sb.Append("T");
                if (Hours.HasValue)
                {
                    sb.Append($"{Hours}H");
                }
                if (Minutes.HasValue)
                {
                    sb.Append($"{Minutes}M");
                }
                if (Seconds.HasValue || Milliseconds.HasValue)
                {
                    sb.Append($"{Seconds ?? 0}");
                    if (Milliseconds.HasValue)
                    {
                        var millisecondsStr = Milliseconds.ToString();
                        while (millisecondsStr.Length < 3)
                        {
                            millisecondsStr = "0" + millisecondsStr;
                        }
                        sb.Append($".{millisecondsStr}");
                    }
                    sb.Append("S");
                }
            }
            return sb.ToString();
        }

        public IqlTimeSpanExpression() : base(IqlExpressionKind.TimeSpan, IqlType.TimeSpan, null)
        {
        }

        public IqlTimeSpanExpression SetDays(int? days)
        {
            Days = days;
            return this;
        }

        public IqlTimeSpanExpression SetHours(int? hours)
        {
            Hours = hours;
            return this;
        }

        public IqlTimeSpanExpression SetMinutes(int? minutes)
        {
            Minutes = minutes;
            return this;
        }

        public IqlTimeSpanExpression SetSeconds(int? seconds)
        {
            Seconds = seconds;
            return this;
        }

        public IqlTimeSpanExpression SetMilliseconds(int? milliseconds)
        {
            Milliseconds = milliseconds;
            return this;
        }

        public IqlTimeSpanExpression Set(int? days = null, int? hours = null, int? minutes = null, int? seconds = null, int? milliseconds = null)
        {
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
            return this;
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlTimeSpanExpression();
			expression.Days = Days;
			expression.Hours = Hours;
			expression.Minutes = Minutes;
			expression.Seconds = Seconds;
			expression.Milliseconds = Milliseconds;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
