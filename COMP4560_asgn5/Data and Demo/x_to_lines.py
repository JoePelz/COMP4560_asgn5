import sys, re

if len(sys.argv) < 3:
		print("Please supply input .x file and output name as command line argument. \ne.g: \"python %s cube.x myCube\"" % sys.argv[0])
		exit()

def readData():
	with open(sys.argv[1]) as f:
		print("Reading %s..." % sys.argv[1])
		data = f.read()
	print("Done reading file")
	return data

def stripComments(data):
	commentPattern = "\/\/.*$"
	result = re.sub(commentPattern, "", data, 0, re.M)
	return result


def parseVertices(data):
	meshPattern = "^Mesh[\s{][^{]*{[^\d]*\d+;[^\d]*(?P<vertices>(?:[\d\-.]+;[\d\-.]+;[\d\-.]+;[,\s\\n]*)+;)"
	matches = re.findall(meshPattern, data, re.M | re.S)
	verts = matches[0].split("\n")
	verts = (i[:-2].split(";") for i in verts)
	return list(verts)

def parseFaces(data):
	facePattern = "^Mesh[\s{].*?;;[^\d]*\d+;[^\d]*((?:\d+;(?:\d+,)+\d+;[,\s]*)+;)"
	matches = re.findall(facePattern, data, re.M | re.S)
	faces = matches[0].split("\n")
	faces = (i[:-1].split(";")[1].split(",") for i in faces)
	return list(faces)

def writePoints(verts):
	print("Writing out file " + sys.argv[2] + "_points.dat")
	#find minimum x value, add it to everthing.
	min = 0.0
	for v in verts:
			if float(v[0]) < min:
				min = float(v[0])
	for v in verts:
		v[0] = str(float(v[0])-min)

	with open(sys.argv[2] + "_points.dat", mode='w') as f:
		for v in verts:
			for component in v:
				f.write(component + " ")
			f.write("\n")
		f.write("-1\n")
	print("Points file written")

def writeLines(faces):
	print("Writing out file " + sys.argv[2] + "_lines.dat")
	lines = set()
	bit = ()
	for face in faces:
		for v in range(len(face)):
			if v == len(face) - 1:
				bit = (face[v], face[0],)
			else:
				bit = (face[v], face[v+1],)
			lines.add(bit)
	with open(sys.argv[2] + "_lines.dat", mode='w') as f:
		for v in lines:
			f.write(str(int(v[0])+0) + " " + str(int(v[1])+0) + "\n")
		f.write("-1\n")
	print("Lines file written")

data = readData()

data = stripComments(data)

vertices = parseVertices(data)
faces = parseFaces(data)

writePoints(vertices)
writeLines(faces)
