# Update the VARIANT arg in devcontainer.json to pick a .NET Core version: 3.1-bionic, 2.1-bionic 
ARG VARIANT="3.1-bionic"
FROM mcr.microsoft.com/dotnet/core/sdk:${VARIANT}

LABEL maintainer="nikiforovall"

# Options for setup script
ARG INSTALL_ZSH="true"
ARG UPGRADE_PACKAGES="false"
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Install needed packages and setup non-root user. Use a separate RUN statement to add your own dependencies.
COPY library-scripts/common-debian.sh /tmp/library-scripts/
RUN /bin/bash /tmp/library-scripts/common-debian.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" \
    && apt-get clean -y && rm -rf /var/lib/apt/lists/* &&  rm -rf /tmp/library-scripts

# Install exercism CLI
RUN wget https://github.com/exercism/cli/releases/download/v3.0.5/exercism-linux-64bit.tgz && \
    tar xzf exercism-linux-64bit.tgz && \
    mv exercism /usr/local/bin/
