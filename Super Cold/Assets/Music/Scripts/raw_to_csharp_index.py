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
    counter = 0
    out = "{ "
    for i, line in enumerate(lines):
        fields = line.split(sep='\t')
        timestamp = fields[0] # eg. 97.523810
        direction = fields[2].strip() # .strip to remove \n. can be 1 or -1
        out += 'else if( z < {}f) \n {{ \n return indices[{}]; \n }} \n'.format(timestamp, counter)
        counter += 1

    out += " };"
    print(out)

if __name__ == '__main__':
    main()
