using UnityEngine;

public class SandRun : MonoBehaviour
{
    /* TODO
     * 1. MAke 2 colors to set at runtime
     * 2. Create a texture of desired size
     * 3. Get compute shader and setup desired structures, probably need to make some sort of logical array
     * 4. Run compute shader to fill texture with two pallete colors
     * 
     * 
     */

    // Colors for dirt:
    // main color
    public Color PColor;
    // dots color
    public Color SColor;
    public int TextureWidth = 512;
    public int TextureHeight = 512;
    public ComputeShader SandShader;
    int sandKernel;

    public RenderTexture renderTexture;

    public struct Pixel
    {
        public Color color;
    }

    Pixel[] map;
    static readonly int HeightID = Shader.PropertyToID("textureHeight");
    static readonly int WidthID = Shader.PropertyToID("textureWidth");
    void Start()
    {
        GenerateMap();
        sandKernel = SandShader.FindKernel("Sand");
        ComputeBuffer mapBuffer = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(float) * 4);
        mapBuffer.SetData(map);
        SandShader.SetFloat("resolution", TextureWidth * TextureHeight);
        SandShader.SetInt(HeightID, TextureHeight);
        SandShader.SetInt(WidthID, TextureWidth);


        renderTexture = new RenderTexture(TextureWidth, TextureHeight, 0);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        SandShader.SetTexture(sandKernel, "Result", renderTexture);
        SandShader.Dispatch(sandKernel, TextureWidth / 8, TextureHeight / 8, 1);
    }

    void GenerateMap()
    {
        map = new Pixel[TextureWidth * TextureHeight];
        for (int x = 0; x < TextureWidth; x++)
        {
            for (int y = 0; y < TextureHeight; y++)
            {
                map[x * TextureWidth + y].color = PColor;
            }
        }
    }

   
}
