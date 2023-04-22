using B83.ExpressionParser;
using ECGameplay;
using UnityEngine;

namespace ECGamePlay
{
    public class ExpressionUtil
    {
        private static ExpressionParser ExpressionParser { get; set; } = new ExpressionParser();


        public static double TryEvaluate(string expressionStr, AttributeComponent attr)
        {
            Expression expression = null;
            try
            {
                expression = ExpressionParser.EvaluateExpression(expressionStr);
                if (expression.Parameters.ContainsKey("攻击力"))
                {
                    expression.Parameters["攻击力"].Value = attr.Attack.Value;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(expressionStr);
                Debug.LogError(e);
            }
            if (expression == null)
                return 0;
            return expression.Value;
        }
    }
}