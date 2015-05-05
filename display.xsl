<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <body>
        <h2>Events:</h2>
        <table>
          <tr bgcolor="#9acd32">
            <th>ID</th>
            <th>Date</th>
            <th>Time</th>
          </tr>
          <xsl:for-each select="events/event">
            <tr>
              <td>
                <xsl:value-of select="id"/>
              </td>
              <td>
                <xsl:value-of select="date"/>
              </td>
              <td>
                <xsl:value-of select="time"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>