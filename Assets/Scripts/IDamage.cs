/// <summary>ダメージしたときに呼ぶばれるインターフェイス</summary>
public interface IDamage
{
    /// <summary>ここにダメージ処理を書きこむ</summary>
    /// <param name="damage">ダメージ値</param>
    void Damage(int damage);
}
