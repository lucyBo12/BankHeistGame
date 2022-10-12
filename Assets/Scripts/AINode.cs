public interface AINode 
{
    public void CallStart();
    public void CallUpdate();
    public void CallEnd();
    public float Weight();

    public AINode NextNode();

}
