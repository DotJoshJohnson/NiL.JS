﻿using System;
using NiL.JS.BaseLibrary;
using NiL.JS.Expressions;
using NiL.JS.Core;

#if !PORTABLE
using NiL.JS.Core.JIT;
using NiL.JS.Statements;
#endif

namespace NiL.JS.Expressions
{
#if !PORTABLE
    [Serializable]
#endif
    public sealed class RegExpExpression : Expression
    {
        private string pattern;
        private string flags;

        public override bool IsContextIndependent
        {
            get
            {
                return false;
            }
        }

        internal override bool ResultInTempContainer
        {
            get { return false; }
        }

        protected internal override PredictedType ResultType
        {
            get
            {
                return PredictedType.Object;
            }
        }

        public RegExpExpression(string pattern, string flags)
        {
            this.pattern = pattern;
            this.flags = flags;
        }

        public static CodeNode Parse(ParsingState state, ref int position)
        {
            var i = position;
            if (!Parser.ValidateRegex(state.Code, ref i, false))
                return null;

            string value = state.Code.Substring(position, i - position);
            position = i;

            state.Code = Tools.RemoveComments(state.SourceCode, i);
            var s = value.LastIndexOf('/') + 1;
            string flags = value.Substring(s);
            try
            {
                return new RegExpExpression(value.Substring(1, s - 2), flags); // объекты должны быть каждый раз разные
            }
            catch (Exception e)
            {
                if (state.message != null)
                    state.message(MessageLevel.Error, CodeCoordinates.FromTextPosition(state.Code, i - value.Length, value.Length), string.Format(Strings.InvalidRegExp, value));
                return new ExpressionWrapper(new ThrowStatement(e));
            }
        }

        protected override CodeNode[] getChildsImpl()
        {
            return null;
        }

        public override JSValue Evaluate(Context context)
        {
            return new RegExp(pattern, flags);
        }

        public override T Visit<T>(Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "/" + pattern + "/" + flags;
        }
    }
}