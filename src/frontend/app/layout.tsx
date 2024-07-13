import "./globals.css";
import type { Metadata } from "next";
import { Open_Sans } from "next/font/google";
import { ClerkProvider } from "@clerk/nextjs";
import { dark } from "@clerk/themes";

import { ThemeProvider } from "@/providers/theme-provider";
import { cn } from "@/lib/utils";

const font = Open_Sans({
  subsets: ["latin"],
  weight: ["300", "400", "500", "600", "800"],
});

export const metadata: Metadata = {
  title: "Synapse",
  description: "Socialize yourself!",
  icons: ["/logo-white.png"],
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <ClerkProvider
      appearance={{
        baseTheme: dark,
      }}>
      <html lang="en" suppressHydrationWarning>
        <body className={cn(font.className)}>
          <ThemeProvider
            attribute="class"
            defaultTheme="dark"
            disableTransitionOnChange>
            {children}
          </ThemeProvider>
        </body>
      </html>
    </ClerkProvider>
  );
}
