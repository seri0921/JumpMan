using UnityEngine;

// ���ɂ�Collider2D���K�{
[RequireComponent(typeof(Collider2D))]
public class SwordController : MonoBehaviour
{
    // ���̌��̎�����i�v���C���[�j
    private Player owner;

    /*void Start()
    {
        // �����̐e�I�u�W�F�N�g����PlayerController�̏����擾���ĕێ�����
        owner = GetComponentInParent<Player>();
        if (owner == null)
        {
            Debug.LogError("���̐e��PlayerController��������܂���I");
        }
    }
    */
    private void OnTriggerStay2D(Collider2D other)
    {

        // �ڐG��������̃^�O�� "sword" ���ǂ������`�F�b�N
        if (other.CompareTag("sword"))
        {
            // ���肩��SwordController�R���|�[�l���g���擾
            SwordController otherSword = other.GetComponent<SwordController>();

            // ���肪�L���Ȍ��ŁA�������傪�����ł͂Ȃ����Ƃ��m�F
            if (otherSword != null && otherSword.owner != this.owner)
            {
                // ������i�v���C���[�j�ɏՓ˂�ʒm����
                //owner.HandleSwordClash();
            }
        }  // ���̃R���C�_�[�����̃g���K�[�ɐG�ꂽ�Ƃ��ɌĂ΂��
           // �G�ꂽ����̃^�O�� "player" ���ǂ������`�F�b�N
        if (other.CompareTag("Player"))
        {
            // �O�̂��߁A�����̎�������U�����Ȃ��悤�Ƀ`�F�b�N����
            // ����(other)�̃��[�g�K�w�ƁA����(this)�̃��[�g�K�w���Ⴄ�ꍇ�̂ݍU��
            
                // ����̃Q�[���I�u�W�F�N�g���V�[������j��i�폜�j����
           // Destroy(other.gameObject);
            other.gameObject.GetComponent<Player>().Die();
            
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
          //  owner.StartSpinAttack();
        }

    }
}