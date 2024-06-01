using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    // Transforms do Avatar
    [Header("Transforms do Avatar")]
    public Transform MainAvatarTransform;  // Transform principal do avatar
    public Transform AvatarHead;           // Transform da cabeça do avatar
    public Transform AvatarBody;           // Transform do corpo do avatar
    public Transform AvatarHand_Left;      // Transform da mão esquerda do avatar
    public Transform AvatarHand_Right;     // Transform da mão direita do avatar

    // Transforms do XR Rig
    [Header("Transforms do XR Rig")]
    public Transform XRHead;               // Transform da cabeça do XR Rig
    public Transform XRHand_Left;          // Transform da mão esquerda do XR Rig
    public Transform XRHand_Right;         // Transform da mão direita do XR Rig

    // Deslocamentos (Offsets)
    [Header("Offsets")]
    public Vector3 headPositionOffset;     // Offset de posição para a cabeça
    public Vector3 handRotationOffset;     // Offset de rotação para as mãos

    // Update é chamado uma vez por frame
    void Update()
    {
        // Sincronizar a posição/rotação da cabeça e do corpo do avatar com o XR Rig
        SyncHeadAndBody();

        // Sincronizar a posição/rotação das mãos do avatar com o XR Rig
        SyncHands();
    }

    // Sincroniza a cabeça e o corpo do avatar com a cabeça do XR Rig
    private void SyncHeadAndBody()
    {
        // Move suavemente a posição do transform principal do avatar para a posição da cabeça do XR mais o offset
        MainAvatarTransform.position = Vector3.Lerp(MainAvatarTransform.position, XRHead.position + headPositionOffset, Time.deltaTime * 5f);

        // Rotaciona suavemente a cabeça do avatar para coincidir com a rotação da cabeça do XR
        AvatarHead.rotation = Quaternion.Lerp(AvatarHead.rotation, XRHead.rotation, Time.deltaTime * 5f);

        // Rotaciona suavemente o corpo do avatar para alinhar com a rotação horizontal da cabeça
        AvatarBody.rotation = Quaternion.Lerp(AvatarBody.rotation, Quaternion.Euler(0, AvatarHead.rotation.eulerAngles.y, 0), Time.deltaTime * 2f);
    }

    // Sincroniza as mãos do avatar com as mãos do XR Rig
    private void SyncHands()
    {
        // Move e rotaciona suavemente a mão direita
        AvatarHand_Right.position = Vector3.Lerp(AvatarHand_Right.position, XRHand_Right.position, Time.deltaTime * 5f);
        AvatarHand_Right.rotation = Quaternion.Lerp(AvatarHand_Right.rotation, XRHand_Right.rotation, Time.deltaTime * 5f) * Quaternion.Euler(handRotationOffset);

        // Move e rotaciona suavemente a mão esquerda
        AvatarHand_Left.position = Vector3.Lerp(AvatarHand_Left.position, XRHand_Left.position, Time.deltaTime * 5f);
        AvatarHand_Left.rotation = Quaternion.Lerp(AvatarHand_Left.rotation, XRHand_Left.rotation, Time.deltaTime * 5f) * Quaternion.Euler(handRotationOffset);
    }
}
