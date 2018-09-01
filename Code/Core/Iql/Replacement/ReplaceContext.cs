using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class RootReferencePath
    {
        public IqlPropertyExpression[] Path { get; set; }
        public IqlRootReferenceExpression Root { get; set; }

        public RootReferencePath(IqlPropertyExpression[] path, IqlRootReferenceExpression root)
        {
            Path = path;
            Root = root;
        }
    }
    public class ReplaceContext
    {
        public Func<ReplaceContext, IqlExpression, IqlExpression> Replacer { get; set; }
        public IqlAncestor Parent => Ancestors.LastOrDefault();
        public IqlAncestor Root => Ancestors.FirstOrDefault();
        public ReplaceContext(Func<ReplaceContext, IqlExpression, IqlExpression> replacer)
        {
            Replacer = replacer;
            Ancestors = new List<IqlAncestor>();
        }

        public virtual IqlExpression Replace(IqlExpression owner, string propertyName, int? index,
            IqlExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            var ancestor = new IqlAncestor(owner, propertyName, index, expression, Parent);
            Ancestors.Add(ancestor);
            var result = expression.ReplaceExpressions(this);
            Ancestors.Remove(ancestor);
            return result;
        }

        public List<IqlAncestor> Ancestors { get; set; }

        public bool IsFromRoot()
        {
            return FindRoot() != null;
        }

        public RootReferencePath FindRoot()
        {
            var path = new List<IqlPropertyExpression>();
            for (var i = Ancestors.Count - 1; i >= 0; i--)
            {
                if (Ancestors[i].Value.Kind == IqlExpressionKind.Property)
                {
                    path.Add(Ancestors[i].Value as IqlPropertyExpression);
                    continue;
                }

                if (Ancestors[i].Value.Kind == IqlExpressionKind.RootReference)
                {
                    return new RootReferencePath(
                        path.ToArray(),
                        Ancestors[i].Value as IqlRootReferenceExpression);
                }
                return null;
            }
            return null;
        }
    }
}