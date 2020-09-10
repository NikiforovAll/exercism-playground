#!/bin/sh
# export EXERCISM_TOKEN=''
token=${EXERCISM_TOKEN}
exercism configure -t=${token} -w=/home/vscode/exercism
