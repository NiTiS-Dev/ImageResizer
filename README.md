# ImageResizer
The utility to resize images easily, scroll down to view several examples

### How to call
#### Windows
Run `cmd` at the directory where ImageResizer.exe is contained
```bash
imageresizer [options]
```
#### Linux
Open terminal at the directory where ImageResizer.exe is contained
```bash
chmod +x ImageResizer
ImageResizer [options]
```

# Examples
## Compress images in half
```cmd
ImageResizer -f "normalCats/*.png" -o "microCats/" --size 0,5x0,5
```
# Parameters
## Unstoppable *Flag* `-u` `--unstoppable` def:false
The application doesn't stop when throwing exceptions
```bash
imageresizer -u
```
## Output *Value* `-o` `--output` def:"out/"
Setup output directory
```bash
imageresizer -o "outputDirectory/images/"
```
## Filter *Value* `-f` `--filter` def:"*.png"
Set search filter for files
```bash
imageresizer -f "C:/Users/MeowNya/Images/*.png"
```
### Nested *Flag* `-n` `--nested` def:true
It creates subdirectories inside the output directory
```
imageresizer -n
```
### Size *Value* `-s` `--size` def:2x2
A new image size in ratio "width to width and height to height"
```
imageresizer -s 4x4
```
