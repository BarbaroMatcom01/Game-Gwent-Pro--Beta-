namespace Interpreted;

public class Parser
{
    private List<Token> tokens;
    private int index = 0;
    private Token currentToken => tokens[index];
    private Token previousToken => tokens[index - 1];

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }
}
