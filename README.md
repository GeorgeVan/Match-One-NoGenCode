# Match-One-NoGenCode

This is a Match-One version totally revised using EntitasGenericAddon.
It's a good place to compare the codegen and non-codegen.

## Core features
  - No Code Gen at all.
  - Features splitted to 4 projects to make the ECS concepts more clear and make code share between platforms feasible.

## See first
  - Entitas
    - https://github.com/sschmid/Entitas-CSharp

  - EntitasGenericAddon
    - https://github.com/c0ffeeartc/EntitasGenericAddon

  - Match-One
    - https://github.com/sschmid/Match-One

## Modifications
1) Entitas from AssetStore.
2) Delete all code-gen related dlls.
3) Copy source from EntitasGenericAddon and make minor changes (see below for details).
4) Delete all generated codes.
5) Update all codes according to EntitasGenericAddon documents.
6) Split features to serval asmdefs for a more clear perspective. (not easy to do this when using code-gen)
7) BoardSystem: If you change the board size from unity inspector, will fully reset the cells. (org version only remove out bounded cells)
8) Add KilledBySysComponent to support above feature (no score when reset).
9) Move BoardComponent to GameStateContext. It's weired for every piece to have a BoardComponent, a singleton.

## Modifications to EntitasGenericAddon
1) Add several easy to use classes and functions.
2) User can set the scanned assemblies. Cause I put all the components in one single asmdef, so can scan this one only.

## About the Cleanup
1) AppStore Version does not support cleanup now.
2) Write a simple function CreateDestorySystem to support this feature.

## About the Chinese characters
Just Ignore them.
