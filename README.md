# ImageResizer
Utility for fast and easily image resizing, scroll down for view several examples

### How to call
#### Windows
Run `cmd` in same directory containing ImageResizer.exe
```bash
imageresizer [options]
```
#### Linux
Open terminal in same directory containing ImageResizer
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
When used - application doesnt stop when throwing exceptions
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
When used - creates subdirectories inside output directory
```
imageresizer -n
```
### Size *Value* `-s` `--size` def:2x2
New image size in ratio width to width and hieght to hieght
```
imageresizer -s 4x4
```
