namespace Interpreted;

abstract class Expression
{
    public abstract TokenType Evaluate();
}

class PrimaryExpression : Expression
{
    public object Value { get; set; }
    public PrimaryExpression(Token value)
    {
        Value = value;
    }
    public override object Evaluate(Token Value)
    {
       object primaryExpression=Value.Value;
       return primaryExpression;
    }
}

class UnaryExpression
{
    public Expression Operand { get; set; }
    public Token Operator { get; set; }

    public UnaryExpression(Expression operand, Token operatorToken)
    {
        Operand = operand;
        Operator = operatorToken;
    }

    public override TokenType Evaluate()
    {
    }
}
class BinaryExpression
{
    public Expression Left { get; set; }
    public Expression Right { get; set; }
    public Token Operator { get; set; }

    public BinaryExpression(Expression left, Token operatorToken, Expression right)
    {
        Left = left;
        Right = right;
        Operator = operatorToken;
    }
    public override object Evaluate()
    {

    }
}
