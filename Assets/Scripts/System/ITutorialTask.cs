
public interface ITutorialTask 
{
    EnemyStateMachine.StateType EnemyType { get;}
    /// <summary>初期化</summary>
    void Init(PlayerInputAction playerInput){}

    /// <summary>チュートリアルのタイトルを取得</summary>
    string GetTitle { get; }

    /// <summary>説明文を取得</summary>
    string GetDescription { get; }

    /// <summary>チュートリアル達成後、次のチュートリアルに遷移するまでの時間</summary>
    float NextTutorialTaskTime { get; }

    /// <summary>チュートリアルが達成されたか判定する</summary>
    /// <returns>Trueだったら達成！</returns>
    bool CheckTask();
}
