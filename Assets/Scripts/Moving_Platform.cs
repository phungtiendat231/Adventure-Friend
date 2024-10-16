using UnityEngine;
using System.Collections;

public class Moving_Platform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.activeInHierarchy) // Kiểm tra xem GameObject có hoạt động không
            {
                StartCoroutine(UnparentPlayer(collision.transform));
            }
            else
            {
                collision.transform.SetParent(null);
            }
        }
    }

    IEnumerator UnparentPlayer(Transform playerTransform)
    {
        yield return new WaitForEndOfFrame();
        playerTransform.SetParent(null);
    }
}