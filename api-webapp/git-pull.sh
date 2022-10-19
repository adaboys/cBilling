# Pull source code
git pull

# Init/Pull submodules, and Checkout main branch on each submodule.
git submodule init
git submodule update --remote --merge
git submodule foreach git checkout main
