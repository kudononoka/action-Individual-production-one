/// <summary>Edge��ڑ��E�ؒf���ɌĂ΂��C���^�[�t�F�C�X�@
/// �e�m�[�h���q�m�[�h�f�[�^��List�ŕۊǂ���ꍇ�Ɏg�p</summary>
public interface IChildNodeSetting
{
    /// <summary>�o�^</summary>
    /// <param name="chileNode">�ۊǂ������q�m�[�h</param>
    void ChildNodeSet(BehaviorTreeBaseNode chileNode);

    /// <summary>����</summary>
    /// <param name="chileNode">�r���������q�m�[�h</param>
    void ChildNodeRemove(BehaviorTreeBaseNode chileNode);
}
