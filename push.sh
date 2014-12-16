if [ "$1" == "" ];
then
	echo "Need comment"
else
	git add * 
	git commit -m "$1"
	git push -u origin master
	echo "Success!"
fi

