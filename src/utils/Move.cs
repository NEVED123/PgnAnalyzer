namespace PgnAnalyzer.Utils;

public class Move{

    public Move(Ply whiteMove, Ply blackMove, int moveNum){
        this.whiteMove = whiteMove;
        this.blackMove = blackMove;
        this.moveNum = moveNum;
    }
    
    public Ply whiteMove {get; set;}
    public Ply blackMove {get; set;}
    public int moveNum {get; set;}

    public override string ToString(){
        return $"{moveNum}. {whiteMove} {blackMove}";
    }

}