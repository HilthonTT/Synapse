"use client";

import Image from "next/image";
import Link from "next/link";
import { formatDistanceToNow } from "date-fns";
import { IconDots, IconPencil, IconTrash, IconX } from "@tabler/icons-react";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

import { useGetUserFromAuth } from "@/features/users/api/queries/use-get-user-from-auth";

import { useDeleteComment } from "@/features/comments/api/mutations/use-delete-comment";
import { useUpdateComment } from "@/features/comments/api/mutations/use-update-comment";

import { CommentValidation } from "@/lib/validation";
import { Loader } from "@/components/loader";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Form, FormControl, FormField, FormItem } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { useToast } from "@/components/ui/use-toast";
import { EmojiPicker } from "@/components/emoji-picker";

type Props = {
  comment: PostComment;
};

export const CommentCard = ({ comment }: Props) => {
  const [isEditing, setIsEditing] = useState(false);
  const [isInDeleteMenu, setIsInDeleteMenu] = useState(false);

  const { toast } = useToast();
  const { data: user, isLoading } = useGetUserFromAuth();
  const { mutateAsync: updateComment, isPending: isUpdating } =
    useUpdateComment(comment.postId);

  const { mutateAsync: deleteComment, isPending: isDeleting } =
    useDeleteComment(comment.postId);

  const isOwner = user?.id === comment.user.userId;

  const form = useForm<z.infer<typeof CommentValidation>>({
    resolver: zodResolver(CommentValidation),
    defaultValues: {
      content: comment.content,
    },
  });

  const handleEdit = () => {
    if (!isOwner) {
      setIsEditing(false);
    }

    setIsEditing(true);
  };

  const toggleDelete = (e: React.MouseEvent) => {
    e.preventDefault();

    if (!isOwner) {
      return;
    }

    setIsInDeleteMenu(true);
  };

  const handleDelete = async () => {
    try {
      await deleteComment(comment.id);

      toast({
        title: "Comment deleted!",
      });
    } catch (error) {
      toast({
        title: "Something went wrong",
        description: "Please try again",
      });
    }
  };

  const onSubmit = async (values: z.infer<typeof CommentValidation>) => {
    try {
      await updateComment({
        commentId: comment.id,
        ...values,
      });

      toast({
        title: "Your comment has been updated!",
      });
    } catch (error) {
      toast({
        title: "Something went wrong",
        description: "Please try again",
      });
    } finally {
      form.reset();
      setIsEditing(false);
    }
  };

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === "Escape") {
        setIsEditing(false);
      }
    };

    window.addEventListener("keydown", handleKeyDown);

    return () => window.removeEventListener("keydown", handleKeyDown);
  }, []);

  useEffect(() => {
    form.reset({
      content: comment.content,
    });
  }, [comment.content, form]);

  return (
    <div className="flex items-center mb-8">
      <Link
        href={`/users/${comment.user.userId}`}
        className="hover:opacity-75 transition">
        <Image
          src={comment.user.imageUrl}
          alt={comment.user.username}
          width={32}
          height={32}
          className="object-cover rounded-full"
        />
      </Link>
      {!isEditing && (
        <div className="flex items-start justify-start ml-2 gap-2 w-full">
          <p className="text-xs font-bold tracking-wider">
            {comment.user.username}
          </p>
          <p className="text-xs tracking-wide line-clamp-1">
            {comment.content}
          </p>
          <p className="text-xs text-gray-500 shrink-0">
            {formatDistanceToNow(comment.createdOnUtc, { addSuffix: true })}
          </p>
        </div>
      )}

      {isEditing && !isLoading && (
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex items-start justify-start w-full mx-4 mr-0">
            <FormField
              control={form.control}
              name="content"
              render={({ field }) => (
                <FormItem className="flex-1">
                  <FormControl>
                    <div className="relative w-full">
                      <Input
                        {...field}
                        placeholder="Edit comment..."
                        disabled={isUpdating}
                      />
                      <div className="absolute top-3 right-3">
                        <EmojiPicker
                          onChange={(emoji) =>
                            field.onChange(`${field.value} ${emoji}`)
                          }
                          disabled={isUpdating}
                        />
                      </div>
                    </div>
                  </FormControl>
                </FormItem>
              )}
            />
          </form>
          <span className="text-[10px]  text-center mt-1 text-neutral-300">
            Press escape to cancel, enter to save
          </span>
        </Form>
      )}

      {isLoading && <Loader className="size-4 mr-2" />}
      {isOwner && (
        <DropdownMenu>
          <DropdownMenuTrigger>
            <Button variant="ghost" className="hover:opacity-75">
              <IconDots size={18} />
              <span className="sr-only">Comment Options</span>
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent side="left" className="space-y-1">
            <DropdownMenuItem
              onClick={handleEdit}
              className="cursor-pointer hover:opacity-75 transition">
              <IconPencil className="mr-2" /> Edit
            </DropdownMenuItem>

            {!isInDeleteMenu ? (
              <DropdownMenuItem
                onClick={toggleDelete}
                className="cursor-pointer hover:opacity-75 transition">
                <IconTrash className="mr-2" /> Delete
              </DropdownMenuItem>
            ) : (
              <>
                <div className="flex items-center justify-between w-full">
                  <DropdownMenuItem
                    onClick={handleDelete}
                    className="cursor-pointer hover:opacity-75 transition">
                    <IconTrash className="mr-2" />
                    Delete
                  </DropdownMenuItem>
                  <DropdownMenuItem
                    onClick={() => setIsInDeleteMenu(false)}
                    className="cursor-pointer hover:opacity-75 transition">
                    <IconX />
                    Cancel
                  </DropdownMenuItem>
                </div>
              </>
            )}
          </DropdownMenuContent>
        </DropdownMenu>
      )}
    </div>
  );
};
