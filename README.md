# ShadowToGensSetConverter
Converts Shadow set data into Sonic Generations compatible set data. Code is not the prettiest but it works:tm:.

NOTE: This does NOT convert all the set data, as some things are not implemented. This includes the checkpoints and the spawn point. Set these yourself manually in your preferred level editor.

## Usage
```
Usage:
ShadowToGensSetConverter.exe <setDifficultyFile> <scaleTransform [0.1]>

setDifficultyFile - The INI file exported from HeroesPowerPlant. 
                    Should be the one marked with the difficulty (_nrm for normal, _hrd for hard)
scaleTransform    - The scale of the resulting set XML. 
                    Defaults to 0.1, which is the recommended scaling for Shadow stages
```

## Requirements
The resulting set data assumes that your Generations stage depends on the following archives:

- cte_cmn
- csc_cmn
- bpc
- sph_cmn

You can edit the game's archive tree in order to add dependencies to these archives.
