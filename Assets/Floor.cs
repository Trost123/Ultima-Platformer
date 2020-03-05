using UnityEngine;

public class Floor : MonoBehaviour
{
    public Floor TheOtherFloor;

    public float fallSpeed;
    public bool TopFloor;

    private float _targetHeight;
    private Transform _backgroundWall;

    private Transform _ladderTransform;
    // Start is called before the first frame update
    void Start()
    {
        float otherFloorHeight = TheOtherFloor.transform.position.y;
        TopFloor = otherFloorHeight < transform.position.y;
        _targetHeight = otherFloorHeight + 8f;
        _ladderTransform = transform.Find("Ladder");
        _backgroundWall = transform.Find("Wall");
        if (!TopFloor)
        {
            _backgroundWall.Translate(0,0, 2f);
        }
    }

    public void OnLevelUp()
    {
        if (!TopFloor)
        {
            float otherFloorHeight = TheOtherFloor.transform.position.y;
            transform.position = new Vector3(0, otherFloorHeight + 30f, transform.position.z + 4f);
            this.TopFloor = true;
            _targetHeight = otherFloorHeight + 8f;
            RamdomizeLadder();
            _backgroundWall.Translate(0,0, -2f);
        }
        else
        {
            TopFloor = false;
            _backgroundWall.Translate(0,0, 2f);
        }
    }

    public void RamdomizeLadder()
    {
        _ladderTransform.position = new Vector3(Random.Range(-27f, 27f), _ladderTransform.position.y, _ladderTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (TopFloor && transform.position.y > _targetHeight) //I'm above the other platform
        {
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
        }

        fallSpeed += Time.deltaTime * 0.012f;
    }
}
