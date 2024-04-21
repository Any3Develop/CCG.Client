namespace CardGame.Services.StateMachine
{
    public class State : IState
    {
        public virtual string Id { get; } = "Empty";

        public State()
        {
            
        }
        public State(string id)
        {
            Id = id;
        }
        
        public virtual void OnEnter(params object[] args){}

        public virtual void OnExit(){}
    }
}