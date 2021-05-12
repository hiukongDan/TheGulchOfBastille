using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class Pixel_Importer : AssetPostprocessor
{
    private bool isAtlas = false;
    private float width, height;
    void OnPreprocessTexture(){
        if(assetPath.Contains(".png")){
            Debug.Log("imported " + assetPath);
            TextureImporter textureImporter = (TextureImporter)assetImporter;

            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.spritePixelsPerUnit = 32f;
            textureImporter.maxTextureSize = 4096;

            // TODO: regex come in and slice the atlas
            Regex rg = new Regex(@"(\d+)x(\d+).png$");
            MatchCollection matches = rg.Matches(assetPath);
            if(matches.Count > 0){
                textureImporter.spriteImportMode = SpriteImportMode.Multiple;
                GroupCollection groups = matches[0].Groups;
                this.width = float.Parse(groups[1].ToString());
                this.height = float.Parse(groups[2].ToString());
                this.isAtlas = true;

                // Debug.Log("width: " + this.width + " height: " + this.height);
            }
            else{
                this.isAtlas = false;
            }
        }
    }

    void OnPostprocessTexture(Texture2D texture){
        if(this.isAtlas){
            Regex rg = new Regex(@"/([^/]+).png$");
            Match match = rg.Match(assetPath);
            string filename = "sprites";
            if(match.Groups.Count > 0){
                filename = match.Groups[1].ToString();
            }

            // Debug.Log(filename);
            int col = Mathf.FloorToInt(texture.width / this.width);
            int row = Mathf.FloorToInt(texture.height / this.height);

            // Debug.Log("texture.width: " + texture.width);
            // Debug.Log("texture.height: " + texture.height);
            // Debug.Log("Calculated size: " + col + " " + row);

            int count = 0;

            List<SpriteMetaData> metaData = new List<SpriteMetaData>();

            for (int i = row-1; i>=0; i--){
                for (int j = 0; j<col; j++){
                    SpriteMetaData data = new SpriteMetaData();
                    data.rect = new Rect(j*this.width, i*this.height, this.width, this.height);
                    data.name = filename + "_" + count;
                    count++;

                    metaData.Add(data);
                }
            }

            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.spritesheet = metaData.ToArray();

            // Debug.Log("Notify: Imported " + filename);

            // Debug.Log(metaData.Count);
        }

    }

    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites){

    }
}
