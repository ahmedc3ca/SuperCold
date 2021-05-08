# Python 3 script to convert the raw audacity timestamps to
# a csharp array
import sys

def main():
    args = sys.argv
    if len(args) != 2:
        print("\nUsage: python3 {} path/to/timestamps_raw.txt\n".format(args[0]))
        exit(-1)
    file = open(args[1], 'r')
    lines = file.readlines()
    out = "{ "
    for i, line in enumerate(lines):
        out += line.split(sep='\t')[0]
        if i < len(lines)-1: # if not last line
            out += ', '
    out += " };"
    print(out)

if __name__ == '__main__':
    main()