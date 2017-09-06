using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog260_project
{
  public class BST
    {
        //my bST top node
      
     BSTnode bstTop;

    public class BSTnode { 
        //BST node class
       
        
            public int BstISBN;
            public BSTnode leftNode;
            public BSTnode rightNode;
            public Book newBook;  //each book will be added with their respective ISBN 

            public BSTnode(int key, Book book)
            {
                BstISBN = key;
                newBook = book;
            }

     }

            //reference to top node
            


//================================================================================
            //adding to the binary tree
            public void Add(int isbnKey, Book myBook)
            {
                //dealing with empty tree
                if (bstTop == null)
                {
                   bstTop = new BSTnode(isbnKey, myBook);  //add key and book with it
                   return; 
                }

                else //if it is not null
                {
                    //start with the top node
                    BSTnode current = bstTop;

                    //go down the tree
                    while (true)  //when new entry finds its position in the tre it breaks out of the code
                    {
                        //check if it is smaller ...wok on left side
                        if (isbnKey < current.BstISBN)
                        {
                            if (current.leftNode == null)
                            {
                                current.leftNode = new BSTnode(isbnKey, myBook);
                                break; //out of the loop
                            }
                            else  //go one node down to the left
                            {
                                current = current.leftNode;
                            }
                        }
                        else if (isbnKey > current.BstISBN)
                        {
                            if (current.rightNode == null)
                            {
                                current.rightNode = new BSTnode(isbnKey, myBook);
                                break;
                            }
                            else //go to the right
                            {
                                current = current.rightNode;
                            }
                        }

                        else //the key is equal to existing isbn
                        {
                            throw new Exception("Duplicate value not allowed. This ISBN already exists");
                        }

                    }

                } //end of else statement
            }  //end of  add method 

            //=============================================================

            //================================================
           // printing the values present in the list
            //print only the ISBNs
      
            


            public List<int> getAllTheISBN()
            {
                List<int> allISBN = new List<int>(); //it will hold all the values

               return  Print(bstTop, allISBN);
            }

            public List<int> Print(BSTnode current, List<int> theISBN )
            {
               // List<int> allISBN = new List<int>();
                if (current == null)
                {
                    return null;
                }

                else
                {
                    Print(current.leftNode, theISBN);
                    theISBN.Add(current.BstISBN);
                    Print(current.rightNode, theISBN);
                }
                return theISBN;

            }
       
      //=================================================================================


            public Book Find(int targetISBN)
            {
                //create a empty book 
                Book empty = new Book();
                empty.Title = "No such book";
                empty.Year = 0;
                empty.Rating=0;
                empty.Author = "";
                
                if (bstTop == null)
                {
                    return empty;   //return the empty book
                }

                else // walk down the tree
                {
                    //set the top node to current
                    BSTnode current= bstTop;
                    //start searching from top
                    while (current != null)
                    {
                        //1.  we found the isbn
                        if (targetISBN ==current.BstISBN )
                        {
                           return current.newBook;  //we will return the book with this ISBN
                        }
                        //2. target isbn is less than the cuurent nodes isbn
                        else if (targetISBN < current.BstISBN)  //it is small so go the left of the tree
                        {
                            current = current.leftNode;
                        }
                        else              //target is bigger so go right
                        {
                            current = current.rightNode;
                        }

                    } //end of the loop

                 } //end of else statement

                return empty; //when nothing is found
            }

      //================================================================================================
 
     // removing a node
      
            public void remove(int keyParam)
            {



                //********************************************************************************************
                // handle case if the tree is empty
                //********************************************************************************************

                if (bstTop == null) // deal with an empty BST
                {
                    throw new ApplicationException("Trying to remove a node from and empty tree.");
                }
                // done with empty tree case





                //********************************************************************************************
                // There is at least one node
                //********************************************************************************************

                // create variables we'll need for the top node or any other node
                BSTnode parentNode = bstTop; // set our walking pointer node to the top node
                BSTnode childNode; // define a node one level down from parent, when we find the right one, 
                // its what we will remove
                bool CameFromParentsLeftPointer = false; // Need to keep track if we got to the child from a parents 
                // left or right pointer, so when we patch up the links, we patch back up to the correct left or right one

                //********************************************************************************************
                // All the rest of this method is 2 large sections, 
                // top section for removing the top node
                // then latter big section is for removing any other node
                //********************************************************************************************
                if (parentNode.BstISBN == keyParam) // are we removing the top node?


                //***************************************************************************************************
                // so here is the first big section dealing with the top node
                // we will not have to walk the tree to find the node, we know its the top node
                // if the top node has 0 or 1 child, we will just adjust the value in the BSTtop pointer
                // but if the top node has 2 children, we will have to walk the tree to find the node to "promote" to the top
                //**************************************************************************************************
                {
                    // There are 3 possibilities for the top node, no children, 1 child, or 2 children
                    //********************************************************************************************
                    // top node has no children
                    //********************************************************************************************
                    if (parentNode.leftNode == null && parentNode.rightNode == null)  // removing the one and only node
                    {
                        bstTop = null;
                        return; // and then leave
                    }

                    //********************************************************************************************
                    // top node has only 1 child
                    //********************************************************************************************
                    // at this point we know the child node has either one or two children, so check if its just one
                    if (parentNode.leftNode != null && parentNode.rightNode == null) // parentNode has only a left node
                    {
                        bstTop = parentNode.leftNode; // put the top node's one child node up into the the top  pointer
                        return; // and then leave
                    }
                    if (parentNode.rightNode != null && parentNode.leftNode == null) // parentNode has only a right node
                    {
                        bstTop = parentNode.rightNode; // put the top node's one child node up into the the top  pointer
                        return; // and then leave
                    }
                    // if we got here, the 2 if's failed, so we know ...

                    //********************************************************************************************
                    //  node has 2 children
                    //********************************************************************************************

                    BSTnode NodeWithKeyValueToOverWrite = parentNode; // save a pointer to the node to be removed by overwriting

                    // walk left then rigth to find largest key on the left side of the BST
                    // move left  first
                    childNode = parentNode.leftNode; // we know we have a left node at this point, as we know we had 2 children

                    // now walk down all right nodes until there are no more
                    CameFromParentsLeftPointer = true;  // we always start from the first left node, so remember this
                    while (childNode.rightNode != null) // now loop down the right
                    {
                        CameFromParentsLeftPointer = false; // if we got inside this while loop, then it is NOT the first left node to remove
                        parentNode = childNode;
                        childNode = parentNode.rightNode;
                    }

                    //*********************************************************************************************
                    // at this point the childNode pointer is a copy of the node we want to copy to the top and then remove
                    // either the bottom far right node or it might have been the lonely 1st left node
                    // as we remove this node, there are only 2 cases, this node has no children, or it has 1 left node
                    // else we would have moved further to the right.
                    //*********************************************************************************************

                    // first see if this node to remove has no children
                    if (childNode.leftNode == null) // if left is null, there are no children <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    {
                        if (CameFromParentsLeftPointer) // this is that one case where it was the first node on left we want to get ride of
                        {
                            parentNode.leftNode = null; // snip off from the parent's left side and its gone
                        }
                        else // must have CameFromParents Right Pointer
                        {
                            parentNode.rightNode = null; // snip off from the parent's right side and its gone
                        }

                        // before we leave, we want to overwrite the NodeWithKeyValueToOverWrite's keyValue (the one we are logically removing)
                        //  with the keyValue of this childNode's keyValue. 
                        NodeWithKeyValueToOverWrite.BstISBN = childNode.BstISBN;// (in this top section, the NodeWithKeyValueToOverWrite is really the top node
                        // but the algorithm works here, and then below in the second section where it won't be the top node we are overwriting.
                        NodeWithKeyValueToOverWrite.newBook = childNode.newBook;   //first place
                    
                    }

                    //*********************************************************************************
                    else  // if hit this else, it means the node to remove DOES have a valid left pointer (but it cannot have a rigth pointer, else we wouldn't be here)
                    {
                        if (CameFromParentsLeftPointer) // this is that one case where it was the first node on left we want to get ride of
                        {
                            parentNode.leftNode = childNode.leftNode; // clone the childs left branch onto the parent's pointer, leaving the child an orphan
                        }
                        else // we had started moving downt he rigth, so we want to wire up this orphan LeftNode to the parent's rigth pointer
                        {
                            parentNode.rightNode = childNode.leftNode; // clone the childs left branch onto the parent's pointer, leaving the child an orphan
                        }

                        // before we leave, we want to overwrite the NodeWithKeyValueToOverWrite's keyValue (the one we thought we
                        // wanted to remove) with the keyValue of this "1 to the left, all the way to the right" node's keyValue.
                        NodeWithKeyValueToOverWrite.BstISBN = childNode.BstISBN;


                        NodeWithKeyValueToOverWrite.newBook = childNode.newBook;  //second place
                    }
                    return;  // we have removed the node


                }
                //********************************************************************************************
                // end of case where we were removing the top node
                //********************************************************************************************

                //********************************************************************************************
                //********************************************************************************************
                //********************************************************************************************


                //********************************************************************************************
                // Now enter the cases where we are removing some node OTHER than the top.
                // code is almost the same as removing the top node, but first we must walk the tree to find 
                // the node to remove, so we might have TWO walk processes
                // first we have to walk the tree to find the node to "remove" (overwrite) 
                // and then if that node has 2 children, we will have to walk the tree the res tof the way down
                // to find the node to "promote" to the top
                //********************************************************************************************

                //**********************************
                // find the node to remove
                //*********************************
                while (true) // loop as we walk down the tree and find the node, (or not)
                {
                    // as we come in, we know the parentNode is not the one we want to remove
                    if (keyParam < parentNode.BstISBN) // if key less than, then we go left
                    {
                        // need to throw exception if there is no left node, means value does not exist in BST
                        if (parentNode.leftNode == null)
                        {
                            throw new ApplicationException("No node with that value in the tree.");
                        }
                        childNode = parentNode.leftNode;
                        CameFromParentsLeftPointer = true;
                    }
                    else // keyParam is greater, so we want to walk to ther rigth to find the node.
                    {
                        // need to throw exception if there is no right node, means value does not exist in BST
                        if (parentNode.rightNode == null)
                        {
                            throw new ApplicationException("No node with that value in the tree.");
                        }
                        childNode = parentNode.rightNode;
                        CameFromParentsLeftPointer = false;
                    }

                    //*********************************************************************************************
                    if (keyParam == childNode.BstISBN) // if the current node has the correct value
                    // begin giant section where we have found that the childnode key is a match, so we want to remove the
                    // child node.  Again, there are the 3 cases, the child has 0, 1, or 2 children.
                    {
                        //********************************************************************************************
                        //  node has no children
                        //********************************************************************************************
                        if (childNode.leftNode == null && childNode.rightNode == null) // true if no children
                        {
                            if (CameFromParentsLeftPointer)
                            {
                                parentNode.leftNode = null; // snip off from the parent's left side and its gone
                            }
                            else  // must have CameFromParents Right Pointer
                            {
                                parentNode.rightNode = null; // snip off from the parent's right side and its gone
                            }

                            return; // and then leave
                        }
                        //********************************************************************************************
                        //  node has only 1 child
                        //********************************************************************************************
                        // we know the child node has either one or two childer 
                        if (childNode.leftNode != null && childNode.rightNode == null) // child has only a left node
                        {
                            if (CameFromParentsLeftPointer)
                            {
                                parentNode.leftNode = childNode.leftNode; // put the child's one child node up into the parents pointer
                            }
                            else  // must have CameFromParents Right Pointer
                            {
                                parentNode.rightNode = childNode.leftNode; ;
                            }
                            return; // and then leave
                        }
                        if (childNode.rightNode != null && childNode.leftNode == null) // child has only a right node
                        {
                            if (CameFromParentsLeftPointer)
                            {
                                parentNode.leftNode = childNode.rightNode; // put the child's one child node up into the parents left pointer
                            }
                            else  // must have CameFromParents Right Pointer
                            {
                                parentNode.rightNode = childNode.rightNode; // put the child's one child node up into the parents right pointer
                            }
                            return; // and then leave
                        }


                        //********************************************************************************************
                        //  If we got here, the node to overwrite has 2 children, so we have to finsih the walk to find the highest value to copy here
                        //********************************************************************************************

                        BSTnode NodeWithKeyValueToOverWrite = childNode; // save a pointer to this node we found that we will remove (replace)
                        parentNode = childNode; //  move the parent pointer down to this node we want to overwrite its value with
                        // and then start the walk first one time to the left, then rigth right right
                        childNode = parentNode.leftNode;
                        CameFromParentsLeftPointer = true;  // need to remember which parent pointer got us here, first time it is the left one
                        while (childNode.rightNode != null) // if we don't enter this while, means it was that first left node we will "promote"
                        {
                            parentNode = childNode;  // otherwise, we start the loop of rigth rigth right
                            childNode = parentNode.rightNode;
                            CameFromParentsLeftPointer = false; // remember we got her from a parents rigth pointer
                        }
                        // when we get here, we reach the highest value node, at the end of the rigth chain (or it might have been the lonely 1st left node).

                        // we will have 2 possibiliiteis, this node to promite had 0 children, or it has one left node (it cannot have a rigth!)

                        // first see if this node to remove has no children
                        if (childNode.leftNode == null) // are there no children?
                        {
                            // yep, there are no children
                            if (CameFromParentsLeftPointer) // this is that one case where it was the first node on left we want to get ride of
                            {
                                parentNode.leftNode = null; // snip off from the parent's left side and its gone
                            }
                            else // we walked the right pointers, so 
                            {
                                parentNode.rightNode = null; // snip off from the parent's right side and its gone
                            }

                            // after removing this bottom of the chain node, we "re-instate it" by copying its value back up to the node
                            // we wanted to "remove", by overwriting the NodeWithKeyValueToOverWrite's keyValue 
                            // with the keyValue of this "1 to the left, all the way to the right" node's keyValue.
                            NodeWithKeyValueToOverWrite.BstISBN = childNode.BstISBN;

                            NodeWithKeyValueToOverWrite.newBook = childNode.newBook; //third place
                        }
                        else  // if hit this else, it means the node to remove has a valid left pointer
                        {
                            if (CameFromParentsLeftPointer) // this is that one case where it was the first node on left we want to get ride of
                            {
                                parentNode.leftNode = childNode.leftNode; // clone the childs left branch onto the parent's pointer, leaving the child an orphan
                            }
                            else // we had turned and had been moving down the chain of rigth pointers
                            {
                                parentNode.rightNode = childNode.leftNode; // clone the childs left branch onto the parent's pointer, leaving the child an orphan
                            }

                            // after removing this bottom of the chain node, we "re-instate it" by copying its value back up to the node
                            // we wanted to "remove", by overwriting the NodeWithKeyValueToOverWrite's keyValue 
                            // with the keyValue of this "1 to the left, all the way to the right" node's keyValue.
                            NodeWithKeyValueToOverWrite.BstISBN = childNode.BstISBN;
                            NodeWithKeyValueToOverWrite.newBook = childNode.newBook;     //fourth place

                        }

                        return;

                    }  // end of if we found the node
                    else   // since we did not find the node with the key value, walk down a node
                    {
                        parentNode = childNode; // move the pointer down to the childnode, and do the while loop again
                        // if the childNode is a null, our existing code above will detect and throw exception
                    }

                }  // End of While loop -------------------------------------------------------------------------------

            }  // end of Remove method



    }
}
