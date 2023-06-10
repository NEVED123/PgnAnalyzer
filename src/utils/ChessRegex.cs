namespace PgnAnalyzer.Utils;

//A bank of regular expressions for recogizing chess moves

public class ChessRegex
{
    //Only includes san with no analysis or annotations
    public static readonly string SanMove = @"([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?";

    //Includes analysis and annotations
    public static readonly string Ply = @"([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?";

    //includes both players moves and the numbers, inclues half turns (white moves and black loses), spaces can be freely added/removed
    public static readonly string Move = @"\d*\.?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?)(\s?(\d+\.{3})?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?))?";

    //includes zero or more moves, and an optional result
    public static readonly string Game = @"(\d*\.?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?)(\s?(\d+\.{3})?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?))?\s?)*(\s?(1-0|0-1|1\/2-1\/2))?";

    //includes one or more moves, and an optional result
    public static readonly string GameWithAtLeastOneMove = @"(\d*\.?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?)(\s?(\d+\.{3})?\s?(([Oo0](-[Oo0]){1,2}|[KQRBN]?[a-h]?[1-8]?x?[a-h][1-8](=[QRBN])?)[#+]?\s?[\??!?]*\s?(\{[^\{\}]*\})?))?\s?)+(\s?(1-0|0-1|1\/2-1\/2))?";

    public static readonly string Result = @"(1-0|0-1|1\/2-1\/2)";

    public static readonly string MoveNum = @"\d+\.{1,}\s";

    public static readonly string WhiteMoveNum = @"\d+\.\s";
    public static readonly string BlackMoveNum = @"\d+\.{3}\s";

    public static readonly string Annotation = @"\{[^\{\}]*\}";

    public static readonly string Analysis = @"[\??!?]{1,2}";

    public static readonly string Tag = @"\[[^\[\]]*\]";
}

