using UnityEngine;

public class Area
{
    float topLine, bottomLine, leftLine, rightLine;
    public Area(float tl, float bl, float ll, float rl)
    {
        this.topLine = tl; this.bottomLine = bl;
        this.rightLine = rl; this.leftLine = ll;    
    }
    public void SetTopLine(float value) { this.topLine = value; }
    public void SetBottomLine(float value) { this.bottomLine = value; }
    public void SetLeftLine(float value) { this.leftLine = value; }
    public void SetRightLine(float value) { this.rightLine = value; }
    public float GetTopLine() { return this.topLine; }
    public float GetBottomLine() { return this.bottomLine; }
    public float GetLeftLine() { return this.leftLine; }

    public float GetRightLine() { return this.rightLine; }
    public Transform GetRandomPosition()
    {
        float randomX = Random.Range(leftLine, rightLine);
        float randomY = Random.Range(bottomLine, topLine);

        GameObject tempObject = new GameObject("RandomPositionObject");
        tempObject.transform.position = new Vector3(randomX, randomY, 0f);
        return tempObject.transform;
    }
}
