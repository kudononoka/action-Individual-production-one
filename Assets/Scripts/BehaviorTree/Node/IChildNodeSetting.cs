/// <summary>Edgeを接続・切断時に呼ばれるインターフェイス　
/// 親ノードが子ノードデータをListで保管する場合に使用</summary>
public interface IChildNodeSetting
{
    /// <summary>登録</summary>
    /// <param name="chileNode">保管したい子ノード</param>
    void ChildNodeSet(BehaviorTreeBaseNode chileNode);

    /// <summary>解除</summary>
    /// <param name="chileNode">排除したい子ノード</param>
    void ChildNodeRemove(BehaviorTreeBaseNode chileNode);
}
