using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class Pixel_Importer : AssetPostprocessor
{
    private bool isAtlas = false;
    private float width, height;
    void OnPreprocessTexture(){
        if(assetPath.Contains(".png")){
            // Debug.Log("imported " + assetPath);
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
            }
            else{
                this.isAtlas = false;
            }
        }
    }

    void OnPostprocessTexture(Texture2D texture){
        if(this.isAtlas){
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            string filename = Path.GetFileNameWithoutExtension(assetPath);

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
                    data.name = filename + "_" + count++;
                    metaData.Add(data);
                    //textureImporter.spritesheet[count-1] = data;
                }
            }
            
            textureImporter.spritesheet = metaData.ToArray();
            // Debug.Log(metaData.Count);

            Debug.Log("Imported " + filename);
            // Debug.Log(metaData.Count);

            // credit - this fix comes from https://gist.github.com/janisgitendorfs/42bb76a8994f50a762dd24aac04e7b53
            AssetDatabase.ForceReserializeAssets(new List<string>{assetPath});
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

    }

    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites){

    }
}
