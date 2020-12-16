using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public Rigidbody2D rbPaddle;

    void Update()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * GameData.PaddleSpeed * Time.deltaTime * horz);
        transform.Translate(Vector2.up * GameData.PaddleSpeed * Time.deltaTime * vert) ;

        if (transform.position.x > GameData.RightEdge) {
            transform.position = new Vector2(GameData.RightEdge, transform.position.y);
        }
        if (transform.position.x < GameData.LeftEdge) {
            transform.position = new Vector2(GameData.LeftEdge, transform.position.y);
        }
        if(transform.position.y > GameData.MaxHeight) {
            transform.position = new Vector2(transform.position.x, GameData.MaxHeight);
        }
        if (transform.position.y < GameData.MinHeight) {
            transform.position = new Vector2(transform.position.x, GameData.MinHeight);
        }
    }
}
